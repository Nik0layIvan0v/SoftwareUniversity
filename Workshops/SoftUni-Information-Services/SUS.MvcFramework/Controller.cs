using SUS.HTTP;
using System.IO;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View(string viewPath)
        {
            byte[] dataBytes = File.ReadAllBytes(viewPath);

            return new HttpResponse("text/html", dataBytes);
        }
    }
}