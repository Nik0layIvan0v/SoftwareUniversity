using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse LoginConfirmed(HttpRequest request)
        {
            //TODO: Read Data
            //TODO: Check User
            //TODO: Log User

            //TODO: Redirect Home Page - DONE!!!

            return this.Redirect("/");
        }
    }
}