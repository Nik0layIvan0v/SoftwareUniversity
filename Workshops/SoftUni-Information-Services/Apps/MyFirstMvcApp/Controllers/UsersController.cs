﻿using MyFirstMvcApp.Data;
using MyFirstMvcApp.Data.EntityModels;
using MyFirstMvcApp.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        private protected readonly IUsersService UsersService = new UsersService();

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost("/users/register")]
        public HttpResponse RegisterData()
        {
            //TODO: Read Data - DONE!!!
            string username = this.HttpRequest.FormData["Username"].ToLower();
            string email = this.HttpRequest.FormData["Email"].ToLower();
            string password = this.HttpRequest.FormData["Password"].Trim();

            //TODO: Validate input from User
            if (email.Contains("@") == false)
            {
                return this.Error("Invalid Email!");
            }

            if (UsersService.IsEmailAlreadyRegistered(email))
            {
                return this.Error("Email is already taken!!");
            }

            if (username.Length > 50 || UsersService.IsUserAlreadyRegistered(username))
            {
                return this.Error("Invalid UserName!");
            }

            if (password.Length > 50)
            {
                return this.Error("Password is too long!");
            }

            //TODO: Register User
            UsersService.CreateUser(username, password, email);

            //TODO: Redirect Login page! - DONE!!!
            return this.Redirect("/home/index");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost("/users/login")]
        public HttpResponse LoginConfirmed()
        {
            //TODO: Read Data - DONE!!!
            string username = this.HttpRequest.FormData["Username"].ToLower();
            string password = this.HttpRequest.FormData["Password"].Trim();

            //TODO: Check User
            if (!UsersService.IsUserValid(username, password))
            {
                return this.Error("Invalid password!");
            }

            //TODO: Log User
            User user = new ApplicationDbContext().Users.FirstOrDefault(x => x.Username == username);

            this.SignIn(user.Id);

            //TODO: Redirect to Home page - DONE!!!
            return this.Redirect("/home/index");
        }

        public HttpResponse LogOut()
        {
            this.SignOut();

            return this.Redirect("/home/index");
        }
    }
}