using MyFirstMvcApp.ViewModels;
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

        [HttpPost]
        public HttpResponse LoginConfirmed()
        {
            //TODO: Read Data - DONE!!!
            //this.HttpRequest.FormData[{name of form input}]

            //TODO: Check User
            LoginUserViewModel testModel = new LoginUserViewModel
            {
                FirstName = this.HttpRequest.FormData["firstName"],
                LastName = this.HttpRequest.FormData["lastName"]
            };

            //TODO: Log User

            //TODO: Redirect Home Page - DONE!!!

            return this.View(testModel);
        }
    }
}