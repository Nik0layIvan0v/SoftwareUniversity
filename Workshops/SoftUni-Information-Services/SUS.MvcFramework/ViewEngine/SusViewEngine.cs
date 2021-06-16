using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SUS.MvcFramework.ViewEngine
{
    public class SusViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel, string user)
        {
            
            //1. Get from Template Code only csharp part
            string csharpCode = GenerateCSharpFromTemplate(templateCode, viewModel);

            //3. Executable obj must have GenerateHtml method witch parameter is the view model.
            IView executableObj = GenerateExecutableCode(csharpCode, viewModel);

            //4. Generated executable object call that method to generate actual ready HTML with filled data.
            string readyHtml = executableObj.GenerateHtml(viewModel, user);

            //5.Return the readyHtml through GetHtml() of SusViewEngine
            return readyHtml;
        }

        private string GenerateCSharpFromTemplate(string template, object viewModel)
        {
            string methodBody = GetMethodBody(template);

            string typeOfModel = "object";

            if (viewModel != null)
            {
                if (viewModel.GetType().IsGenericType)
                {
                    string modelName = viewModel.GetType().FullName; // => example: List<string> will be List`1

                    Type[] genericArguments = viewModel.GetType().GenericTypeArguments;// => List<string> args: string... Dictionary<string,int> args: string, int... etc.

                    typeOfModel = modelName.Substring(0,modelName.IndexOf('`')); // List`1 => Substring() => List

                    string splitArguments = string.Join(',', genericArguments.Select(v=>v.Name).ToList());

                    typeOfModel = typeOfModel + "<" + $"{splitArguments}" + ">";

                }
                else
                {
                    typeOfModel = viewModel.GetType().FullName;
                }
            }

            string csharpCode = @"
                               using System;
                               using System.Text;
                               using System.Linq;
                               using System.Collections.Generic;
                               using SUS.MvcFramework.ViewEngine;
                                
                                namespace ViewNamespace
                                {
                                    public class ViewClass : IView
                                    {
                                         public string GenerateHtml(object viewModel, string user)
                                         {
                                            var User = user;
                                            var Model = viewModel as " + $"{typeOfModel};" + @"

                                            StringBuilder result = new StringBuilder();

                                            " + $"{methodBody}" + @"

                                            return result.ToString().Trim();
                                         }
                                    }
                                }
                                ";
            return csharpCode;
        }

        private string GetMethodBody(string template)
        {
            ICollection<string> supportedOperations = new List<string>
            {
                "if","else","for","foreach","while"
            };

            StringReader reader = new StringReader(template);

            StringBuilder builder = new StringBuilder();

            while (true)
            {
                string currentLine = reader.ReadLine();

                if (currentLine == null)
                {
                    break;
                }

                if (supportedOperations
                        .Any(supportedOperator => currentLine.TrimStart().StartsWith("@" + supportedOperator)))
                {
                    int atSignIndex = currentLine.IndexOf('@');

                    currentLine = currentLine.Remove(atSignIndex, 1);

                    builder.AppendLine(currentLine);
                }
                else if (currentLine.TrimStart().StartsWith('{') || currentLine.TrimStart().StartsWith('}'))
                {
                    builder.AppendLine(currentLine);
                }
                else
                {
                    Regex matchEndOfCSharpCode = new Regex(@"[^\""\s&\'\<]+");

                    builder.Append($"result.AppendLine(@\""); // => result.AppendLine(@"

                    while (currentLine.Contains('@'))
                    {
                        int atSignIndex = currentLine.IndexOf('@');

                        string htmlBeforeAtSign = currentLine //EXAMPLE: <li class="example">@variable</li>
                            .Substring(0, atSignIndex) //Substring() => "<li class="example">"
                            .Replace("\"", "\"\""); //Replace() => "<li class=""example"">" escaped html before @

                        builder.Append(htmlBeforeAtSign + "\"" + "+"); //Append() => result.AppendLine("<li class=""example"">" +

                        string lineAfterAtSign = currentLine //EXAMPLE: <li class="example">@variable</li>
                            .Substring(atSignIndex + 1); //Substring() => variable</li>

                        string csharpCode = matchEndOfCSharpCode //EXAMPLE: variable</li>
                            .Match(lineAfterAtSign).Value; //Match() => variable

                        builder
                            .Append(csharpCode + "+ \""); //Append() => result.AppendLine("<li class=""example"">" + variable + "

                        currentLine = lineAfterAtSign.Substring(csharpCode.Length);

                    }

                    builder //AppendLine() => result.AppendLine("<li class=""example"">" + variable + "</li>");
                        .AppendLine($"{currentLine.Replace("\"", "\"\"")}\");");
                }
            }

            return builder.ToString();
        }

        //2. Get only compile part from the csharp code;
        private static IView GenerateExecutableCode(string csharpCode, object viewModel)
        {
            CSharpCompilation compileResult = CSharpCompilation
                .Create("ViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

            if (viewModel != null)
            {
                if (viewModel.GetType().IsGenericType)
                {
                    Type[] genericArguments = viewModel.GetType().GenericTypeArguments;

                    foreach (var argument in genericArguments)
                    {
                        compileResult = compileResult.AddReferences(MetadataReference.CreateFromFile(argument.Assembly.Location));
                    }
                }

                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(viewModel.GetType().Assembly.Location));
            }

            AssemblyName[] libraries = Assembly.Load(new AssemblyName("netstandard")).GetReferencedAssemblies();

            foreach (var library in libraries)
            {
                compileResult = compileResult
                    .AddReferences(MetadataReference.CreateFromFile(Assembly.Load(library).Location));
            }

            compileResult = compileResult.AddSyntaxTrees(SyntaxFactory.ParseSyntaxTree(csharpCode));

            using MemoryStream memoryStream = new MemoryStream();

            EmitResult emitResult = compileResult.Emit(memoryStream);

            if (emitResult.Success == false)
            {
                IEnumerable<string> errorList = emitResult.Diagnostics
                    .Where(x => x.Severity == DiagnosticSeverity.Error)
                    .Select(x => x.GetMessage());

                return new ErrorView(errorList, csharpCode);
            }

            try
            {
                memoryStream.Seek(0, SeekOrigin.Begin);

                byte[] rawAssembly = memoryStream.ToArray();

                Assembly viewAssembly = Assembly.Load(rawAssembly);

                var viewType = viewAssembly.GetType("ViewNamespace.ViewClass");

                object instance = Activator.CreateInstance(viewType);

                return instance as IView;
            }
            catch (Exception exception)
            {
                IEnumerable<string> errorMessages = new List<string>
                {
                   exception.Message.ToString()
                };

                return new ErrorView(errorMessages, csharpCode);
            }
        }
    }
}