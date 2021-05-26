using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    //Other stuffs like about page (no logic)
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse About(HttpRequest request)
        {
            return this.View();
        }
    }
}