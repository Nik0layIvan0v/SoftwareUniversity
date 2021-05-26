using SUS.HTTP;
using System.IO;
using System.Runtime.CompilerServices;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View([CallerMemberName] string actionName = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);

            byte[] dataBytes = System.IO.File.ReadAllBytes($"Views/{controllerName}/{actionName}.html");

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