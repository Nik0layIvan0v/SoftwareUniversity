using MyFirstMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            //Property data may come from Database/Services/Repositories or else;
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "WELCOME USER!";

            if (this.IsUserSignedIn() == false)
            {
                viewModel.Message = "User is not logged in!";
            }

            return this.View(viewModel);
        }

        public HttpResponse About()
        {
            List<int> viewModel = new List<int>
            {
                1,2,3,4,5
            };

            return this.View(viewModel);
        }

        public HttpResponse AutoLogin()
        {
            this.SignIn("niki");

            return this.Redirect("/home/index");
        }

        public HttpResponse AutoLogOut()
        {
            this.SignOut();

            return this.Redirect("/home/index");
        }
    }
}