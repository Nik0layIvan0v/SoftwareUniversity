﻿using MyFirstMvcApp.Data;
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
    }
}