namespace SUS.MvcFramework.ViewEngine
{
    public class SusViewEngine : IViewEngine
    {
        public string GetHtml(string templateCode, object viewModel)
        {
            //TODO: Implement logic
            
            string csharpCode = GenerateCSharpFromTemplate(templateCode);

            //3. Executable obj must have GenerateHtml method witch parameter is the view model.
            IView executableObj = GenerateExecutableCode(csharpCode);

            string readyHtml = executableObj.GenerateHtml(viewModel);

            //4. Generated executable object call that method to generate actual ready HTML and return it through GetHtml()
            return readyHtml;
        }

        //1. Get from Template Code only csharp part
        public string GenerateCSharpFromTemplate(string template)
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