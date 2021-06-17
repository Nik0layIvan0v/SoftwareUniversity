using MyFirstMvcApp.Data;
using MyFirstMvcApp.Data.EntityModels;
using MyFirstMvcApp.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;
using MyFirstMvcApp.ViewModels.Users;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        private protected readonly IUsersService UsersService;

        public UsersController(IUsersService usersService)
        {
            this.UsersService = usersService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost("/users/register")]
        public HttpResponse RegisterData(InputUserTestModel testRegisterModel)
        {
            //TODO: Read Data - DONE!!!
            //string username = this.HttpRequest.FormData["Username"].ToLower(); <= Now is data bidden!

            //TODO: Validate input from User
            if (testRegisterModel.Email.Contains("@") == false)
            {
                return this.Error("Invalid Email!");
            }

            if (UsersService.IsEmailAlreadyRegistered(testRegisterModel.Email))
            {
                return this.Error("Email is already taken!!");
            }

            if (testRegisterModel.Username.Length > 50 || UsersService.IsUserAlreadyRegistered(testRegisterModel.Username))
            {
                return this.Error("Invalid UserName!");
            }

            //if (testRegisterModel.Password.Length > 50)
            //{
            //    return this.Error("Password is too long!");
            //}

            //TODO: Register User
            //UsersService.CreateUser(username, password, email);

            //TODO: Redirect Login page! - DONE!!!
            return this.Redirect("/");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost("/users/login")]
        public HttpResponse LoginConfirmed(string username, string password)
        {
            if (IsUserSignedIn() == true)
            {
                return this.Error("User is already logged in"); ;
            }

            //TODO: Read Data - DONE!!!
            //string username = this.HttpRequest.FormData["Username"].ToLower();
            //string password = this.HttpRequest.FormData["Password"].Trim();

            //TODO: Check User
            if (!UsersService.IsUserValid(username, password))
            {
                return this.Error("Invalid password!");
            }

            //TODO: Log User
            User user = new ApplicationDbContext().Users.FirstOrDefault(x => x.Username == username);

            this.SignIn(user.Id);

            //TODO: Redirect to Home page - DONE!!!
            return this.Redirect("/");
        }

        public HttpResponse LogOut()
        {
            if (IsUserSignedIn() == false)
            {
                return this.Error("User is already signed out");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}