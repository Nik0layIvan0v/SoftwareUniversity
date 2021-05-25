using SUS.HTTP;
using SUS.MvcFramework;
using System.IO;
using System.Text;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Users(HttpRequest request)
        {
            //=============BEFORE===========
            byte[] responseBytes = Encoding.UTF8.GetBytes("<h1>Users()</h1>");

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }

        public HttpResponse Register(HttpRequest request)
        {
            //=============BEFORE===========

            byte[] responseBytes = File.ReadAllBytes(@"Views\Users\Register.html");

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }

        public HttpResponse Login(HttpRequest request)
        {
            //=============AFTER============
            return this.View(@"Views\Users\Login.html");
        }


    }
}