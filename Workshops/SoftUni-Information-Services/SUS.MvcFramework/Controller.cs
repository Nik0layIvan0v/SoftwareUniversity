using SUS.HTTP;
using System.Runtime.CompilerServices;
using System.Text;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View([CallerMemberName] string actionName = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);

            string viewContent = System.IO.File.ReadAllText($"Views/{controllerName}/{actionName}.html");

            string layout = System.IO.File.ReadAllText("Views/Shared/_Layout.html");

            string combinationLayoutAndViewContent = layout.Replace("@RenderBody()", viewContent);

            byte[] dataBytes = Encoding.UTF8.GetBytes(combinationLayoutAndViewContent);

            return new HttpResponse("text/html", dataBytes);
        }

        public HttpResponse File(string filePath, string contentType)
        {
            HttpResponse response = new HttpResponse(contentType,new byte[0]);

            if (System.IO.File.Exists(filePath))
            {
                byte[] data = System.IO.File.ReadAllBytes(filePath);
                response = new HttpResponse(contentType, data);
            }

            return response;
        }
    }
}