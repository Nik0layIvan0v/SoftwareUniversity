using MyFirstMvcApp.Data;
using MyFirstMvcApp.Data.EntityModels;
using MyFirstMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();

        public HttpResponse Register()
        {
            //TODO: Read Data - DONE!!!
            //this.HttpRequest.FormData[{name of form input}]

            //TODO: Check User

            //TODO: Log User

            //TODO: Redirect Home Page - DONE!!!

            return this.View();
        }

        public HttpResponse Login()
        {
            LoginUserViewModel[] models = dbContext.Users
                .Select(user => new LoginUserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName
                })
                .ToArray();

            return this.View(models);
        }

        [HttpPost]
        public HttpResponse LoginConfirmed()
        {
            //TODO: Read Data - DONE!!!
            //this.HttpRequest.FormData[{name of form input}]

            //TODO: Check User

            //TODO: Log User

            //TODO: Redirect - DONE!!!

            if (this.HttpRequest.FormData["firstName"].Length > 20)
            {
                return this.Error("Name should not exceed 20 Symbols");
            }

            dbContext.Users.Add(new User
            {
                FirstName = this.HttpRequest.FormData["firstName"],
                LastName = this.HttpRequest.FormData["lastName"]
            });

            dbContext.SaveChanges();

            return this.Redirect("/users/login");
        }
    }
}