using SUS.HTTP;
using System.IO;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View(string viewPath)
        {
            byte[] dataBytes = System.IO.File.ReadAllBytes(viewPath);

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