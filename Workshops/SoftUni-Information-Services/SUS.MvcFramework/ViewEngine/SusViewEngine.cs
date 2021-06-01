using System.Text;

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
            IView executableObj = GenerateExecutableCode(csharpCode);

            //4. Generated executable object call that method to generate actual ready HTML with filled data.
            string readyHtml = executableObj.GenerateHtml(viewModel);

            //5.Return the readyHtml through GetHtml() of SusViewEngine
            return readyHtml;
        }

        private string GenerateCSharpFromTemplate(string template)
        {
            string csharpCode = @"
                                using System
                                using System.Text;
                                using System.Linq
                                using System.Collections.Generic;
                                using SUS.MvcFramework.ViewEngine;
                                
                                namespace ViewNamespace
                                {
                                    public class ViewClass : IView
                                    {
                                         public string GenerateHtml(object viewModel)
                                         {
                                            StringBuilder htmlBuilder = new StringBuilder();
                                            
                                            {methodBody}

                                            return htmlBuilder.ToString();
                                         }
                                    {
                                }
                                ";

            string methodBody = GetMethodBody(template);

            string result = csharpCode.Replace(@"{methodBody}", methodBody);

            return result;
        }

        private string GetMethodBody(string template)
        {
            throw new System.NotImplementedException();
        }

        //2. Get only compile part from the csharp code;
        private IView GenerateExecutableCode(string csharpCode)
        {
            throw new System.NotImplementedException();
        }
    }
}