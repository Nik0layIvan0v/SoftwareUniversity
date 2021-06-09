using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Register()
        {
            return this.View();
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        public HttpResponse LoginConfirmed()
        {
            //TODO: Read Data
            //TODO: Check User
            //TODO: Log User

            //TODO: Redirect Home Page - DONE!!!

            return this.Redirect("/");
        }
    }
}