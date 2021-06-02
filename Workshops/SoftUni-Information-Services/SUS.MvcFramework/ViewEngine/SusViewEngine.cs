using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis.Emit;

namespace SUS.MvcFramework.ViewEngine
{
    public class SusViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            //TODO: Implement logic

            //1. Get from Template Code only csharp part
            string csharpCode = GenerateCSharpFromTemplate(templateCode);

            //3. Executable obj must have GenerateHtml method witch parameter is the view model.
            IView executableObj = GenerateExecutableCode(csharpCode, viewModel);

            //4. Generated executable object call that method to generate actual ready HTML with filled data.
            string readyHtml = executableObj.GenerateHtml(viewModel);

            //5.Return the readyHtml through GetHtml() of SusViewEngine
            return readyHtml;
        }

        private string GenerateCSharpFromTemplate(string template)
        {
            string methodBody = GetMethodBody(template);

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
                                         public string GenerateHtml(object viewModel)
                                         {
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
            StringReader reader = new StringReader(template);

            StringBuilder builder = new StringBuilder();

            while (true)
            {
                string currentLine = reader.ReadLine();

                if (currentLine == null)
                {
                    break;
                }

                string escapedSymbolsLine = currentLine.Replace("\"", "\"\"");

                builder.AppendLine($"result.AppendLine(@\"{escapedSymbolsLine}\");");

            }

            return builder.ToString();
        }

        //2. Get only compile part from the csharp code;
        private IView GenerateExecutableCode(string csharpCode, object viewModel)
        {
            CSharpCompilation compileResult = CSharpCompilation
                .Create("ViewAssembly")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddReferences(MetadataReference.CreateFromFile(typeof(IView).Assembly.Location));

            if (viewModel != null)
            {
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

                Type viewType = viewAssembly.GetType("ViewNamespace.ViewClass");

                var instance = Activator.CreateInstance(viewType);

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