using System.Text.RegularExpressions;
using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using Git.Data.DataValidationConstants;
using Git.Services.Users;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            this._service = service;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            return this.View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            string userId = this._service.GetUserId(username, password);

            if (userId == null)
            {
                return Error("Invalid username or password");
            }

            this.SignIn(userId);

            //Upon successful Login of a User, you should be redirected to the /Repositories/All.
            return this.Redirect("/Repositories/All");
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
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            if (password != confirmPassword)
            {
                return this.Error("Password and Confirm password are not the same.");
            }

            if (!Regex.IsMatch(email, UserRegistrationConstraints.EmailRegularExpressionPattern))
            {
                return this.Error("Email is not valid");
            }

            if (_service.IsEmailAvailable(email))
            {
                return this.Error("Email is already taken!");
            }

            if (_service.IsUsernameAvailable(username))
            {
                return this.Error("Username is already taken!");
            }

            if (username.Length < 5 || username.Length > 20)
            {
                return this.Error("Invalid Username: must be between with min length: 5 and max length: 20");
            }

            if (password.Length < 6 || password.Length > 20)
            {
                return this.Error("Invalid Password: must be between with min length: 6 and max length: 20");
            }

            if (string.IsNullOrWhiteSpace(username) || 
                string.IsNullOrWhiteSpace(email) || 
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                return this.Error("Fields cannot be empty");
            }

            _service.CreateUser(username, email, password);

            //Upon successful Registration of a User, you should be redirected to the Login Page.
            return this.Redirect("/Users/Login");
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