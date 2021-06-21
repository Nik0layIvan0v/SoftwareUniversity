using Musaca.Data.EntityModels;
using Musaca.Models.UserModels;
using Musaca.Services.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Musaca.Controllers
{
    public class UsersController : Controller
    {
        private protected readonly IUsersService UsersService;

        public UsersController(IUsersService usersService)
        {
            this.UsersService = usersService;
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
        public HttpResponse Login(LoginUserInputModel userInputData)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            User userId = this.UsersService.GetUserFromDb(userInputData);

            if (userId == null)
            {
                return this.Error("Invalid Username or password");
            }

            string uid = userId.Id;
            string userRole = userId.Role.ToString();
            string username = userId.Username;

            this.SignIn(uid, userRole, username);

            return this.Redirect("/");
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
        public HttpResponse Register(RegisterUserInputModel userInputData)
        {
            //1. Session Validations!
            if (this.IsUserSignedIn())
            {
                return this.Error("You are already logged in!");
            }

            //2. Input Validations!
            if (userInputData.Password != userInputData.ConfirmPassword)
            {
                return this.Error("Password and Confirm Password are not Equal!");
            }

            //TODO: check for null, invalid email, input length (from problem description)

            //3. Service Validations!
            bool isUserAlreadyRegistered = this.UsersService.IsUsernameRegister(userInputData.Username);

            if (isUserAlreadyRegistered)
            {
                return this.Error($"Username: '{userInputData.Username}' already exist in the Database!");
            }

            bool isEmailAlreadyRegistered = this.UsersService.IsEmailRegister(userInputData.Email);

            if (isEmailAlreadyRegistered)
            {
                return this.Error($"Email: '{userInputData.Email}' already exist in the Database!");
            }

            //4. Register new User in database!
            this.UsersService.CreateUser(userInputData);

            //5. Redirect to login!
            return this.Redirect("/");
        }


        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return this.Error("User is already Logged out!");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}
