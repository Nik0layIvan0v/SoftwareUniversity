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

        public HttpResponse Popper(HttpRequest httpRequest)
        {
            return this.File("wwwRoot/js/popper.min.js", "text/javascript");
        }

        public HttpResponse BootstrapCss(HttpRequest httpRequest)
        {
            return this.File("wwwRoot/css/bootstrap.min.css", "text/css");
        }

        public HttpResponse Jquery(HttpRequest httpRequest)
        {
             return this.File("wwwRoot/js/jquery-3.4.1.min.js", "text/javascript");
        }

        public HttpResponse BootstrapJs(HttpRequest httpRequest)
        {
             return this.File("wwwRoot/js/bootstrap.min.js", "text/javascript");
        }
    }
}