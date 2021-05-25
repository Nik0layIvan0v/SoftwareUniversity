using SUS.HTTP;
using System.IO;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            byte[] faviconBytes = File.ReadAllBytes(@"wwwRoot/favicon.ico");

            HttpResponse response = new HttpResponse("image/vnd.microsoft.icon", faviconBytes);

            return response;
        }
    }
}
