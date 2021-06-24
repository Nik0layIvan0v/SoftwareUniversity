using System.Linq;
using System.Text.RegularExpressions;
using BattleCards.Models;
using BattleCards.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using static BattleCards.Data.DataConstants;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private protected readonly UsersService UserService;

        public UsersController(UsersService userService)
        {
            this.UserService = userService;
        }

        //GET
        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserInputModel formModel)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            if (formModel.Password != formModel.ConfirmPassword)
            {
                return this.Error("Password and confirm password are different!");
            }

            if (this.UserService.IsUsernameAlreadyRegistered(formModel.Username))
            {
                return this.Error($"Username {formModel.Username} is already registered!");
            }

            if (Regex.IsMatch(formModel.Email, EmailRegularExpression) == false)
            {
                return this.Error($"Email: {formModel.Email} is not valid!");
            }

            if (formModel.Username.Length > MaxUsernameLength ||
                formModel.Username.Length < MinUsernameLength)
            {
                return this.Error(
                    $"Username: {formModel.Username} must be between {MinUsernameLength} and {MaxUsernameLength} characters long!");
            }

            if (formModel.Username.Count(char.IsLetterOrDigit) < MinUsernameLength)
            {
                return this.Error($"Username: {formModel.Username} cannot contain whitespaces!");
            }

            if (formModel.Password.Length > MaxPasswordLength || formModel.Password.Length < MinPasswordLength)
            {
                return this.Error($"Password must be between {MinPasswordLength} and {MaxPasswordLength} characters long!");
            }

            this.UserService.CreateUser(formModel);

            return this.Redirect("/Users/Login");
        }

        //GET
        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserInputModel loginUser)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            string userId = this.UserService.GetUserId(loginUser);

            if (userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);

            return this.Redirect("/Cards/All");
        }

        //GET
        [HttpGet("/Logout")]
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