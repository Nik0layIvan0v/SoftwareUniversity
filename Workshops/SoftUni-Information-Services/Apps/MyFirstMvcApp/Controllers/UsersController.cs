using System.Collections.Generic;
using System.Linq;
using MyFirstMvcApp.Data;
using MyFirstMvcApp.Data.EntityModels;
using MyFirstMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

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
            var models = dbContext.Users
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

            //TODO: Redirect Home Page - DONE!!!

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