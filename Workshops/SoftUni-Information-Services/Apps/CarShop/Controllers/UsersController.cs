using System.Linq;
using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Text.RegularExpressions;
using static CarShop.Data.DataConstants.UsernameValidConstraints;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private protected readonly UsersService Service;

        public UsersController(UsersService service)
        {
            this.Service = service;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            string userId = this.Service.GetUserId(username, password);

            if (userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);

            return this.Redirect("/Cars/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputUserModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            if (model.Username.Length < MinUsernameLength ||
                model.Username.Length > DefaultMaxLength ||
                string.IsNullOrWhiteSpace(model.Username))
            {
                return this.Error($"Username: {model.Username} is invalid! Required min length is {MinUsernameLength} characters and max length is {DefaultMaxLength} characters!");
            }

            if (this.Service.IsUsernameAvailable(model.Username))
            {
                return this.Error($"Username: {model.Username} is already registered!");
            }

            if (!Regex.IsMatch(model.Email, EmailRegex))
            {
                return this.Error($"Email: {model.Email} is not valid!");
            }

            if (model.Password.Length < PasswordMinLength ||
                model.Password.Length > DefaultMaxLength ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                return this.Error(
                    $"invalid password! Password must be between {PasswordMinLength} and {DefaultMaxLength} length!");
            }

            if (model.Password.All(x => x == ' '))
            {
                return this.Error($"The provided password cannot be only whitespaces!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error($"Password and confirm password are different!");
            }

            this.Service.Create(model);

            return this.Redirect("/users/login");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return this.Redirect("/");
        }
    }
}