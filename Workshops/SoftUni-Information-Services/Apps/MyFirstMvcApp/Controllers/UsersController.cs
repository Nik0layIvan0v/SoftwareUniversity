using System.IO;
using System.Text;
using SUS.HTTP;
using SUS.MvcFramework;

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

        public HttpResponse Login(HttpRequest request)
        {
            //=============AFTER============
            byte[] responseBytes = File.ReadAllBytes(@"Views\Users\Login.html");

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }

        public HttpResponse Register(HttpRequest request)
        {
            string responseHtml = "<h1>Register()</h1>";

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }
    }
}