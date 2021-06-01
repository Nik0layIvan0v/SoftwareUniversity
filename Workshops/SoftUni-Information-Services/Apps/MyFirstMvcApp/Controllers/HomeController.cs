using System;
using System.Linq;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            if (request.Headers.FirstOrDefault(x => x.Name == "ivan") != null)
            {
                Console.WriteLine("ivan is logged in the system.");
            }

            return this.View();
        }

        public HttpResponse About(HttpRequest request)
        {
            return this.View();
        }
    }
}