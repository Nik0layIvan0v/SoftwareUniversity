using System.Linq;
using CarShop.Services;
using CarShop.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Text.RegularExpressions;
using static CarShop.Data.DataConstants.DataConstants;

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

            if (model.Username.Length < UserMinUsernameLength ||
                model.Username.Length > DefaultMaxLength ||
                string.IsNullOrWhiteSpace(model.Username))
            {
                return this.Error($"Username: {model.Username} is invalid! Required min length is {UserMinUsernameLength} characters and max length is {DefaultMaxLength} characters!");
            }

            if (this.Service.IsUsernameAvailable(model.Username))
            {
                return this.Error($"Username: {model.Username} is already registered!");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
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

            if (model.Password.Select(char.IsLetterOrDigit).Count() < PasswordMinLength)
            {
                return this.Error($"The provided password cannot contain whitespaces!");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error($"Password and confirm password are different!");
            }

            if (model.UserType != UserTypeMechanic && model.UserType != UserTypeClient)
            {
                return this.Error($"User can be only {UserTypeMechanic} or {UserTypeClient}");
            }

            this.Service.Create(model);

            return this.Redirect("/users/login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are already logged out!");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}