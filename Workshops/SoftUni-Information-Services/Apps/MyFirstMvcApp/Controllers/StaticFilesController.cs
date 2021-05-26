using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest request)
        {
            return this.File("wwwRoot/favicon.ico", "image/vnd.microsoft.icon");
        }
    }
}