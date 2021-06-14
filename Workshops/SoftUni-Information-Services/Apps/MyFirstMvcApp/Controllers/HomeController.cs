using MyFirstMvcApp.Data;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;
using MyFirstMvcApp.ViewModels;
using MyFirstMvcApp.Data.EntityModels;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private protected readonly ApplicationDbContext db = new ApplicationDbContext();

        public HttpResponse Index()
        {
            //LoggedUserViewModel userViewModel = null;

            if (this.IsUserSignedIn())
            {
                string currentlyLoggedUserId = this.GetUserId();

                User databaseUser = db.Users.FirstOrDefault(x => x.Id == currentlyLoggedUserId);

                var userViewModel = new LoggedUserViewModel
                {
                    Username = databaseUser.Username,
                    Email = databaseUser.Email
                };

                return this.View(userViewModel);
            }

            return this.View();
        }

        public HttpResponse About()
        {
            return this.View();
        }

        public HttpResponse AutoLogin()
        {
            if (IsUserSignedIn() == true)
            {
                return this.Error("User is already logged in"); ;
            }

            this.SignIn(db.Users.FirstOrDefault()?.Id);

            return this.Redirect("/home/index");
        }

        public HttpResponse AutoLogOut()
        {
            if (IsUserSignedIn() == false)
            {
                return this.Error("User is already signed out");
            }

            this.SignOut();

            return this.Redirect("/home/index");
        }
    }
}