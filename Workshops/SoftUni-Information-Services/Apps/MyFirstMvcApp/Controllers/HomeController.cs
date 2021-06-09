using MyFirstMvcApp.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            //Property data may come from Database/Services/Repositories or else;
            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "I am the message from the ViewModel";

            return this.View(viewModel);
        }

        public HttpResponse About()
        {
            return this.View();
        }
    }
}