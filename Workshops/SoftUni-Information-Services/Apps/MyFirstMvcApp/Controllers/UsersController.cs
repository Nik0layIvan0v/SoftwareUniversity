using System.Text;
using SUS.HTTP;

namespace MyFirstMvcApp.Controllers
{
    public class UsersController
    {
        public HttpResponse Users(HttpRequest request)
        {
            string responseHtml = "<h1>Users()</h1>";

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }

        public HttpResponse Login(HttpRequest request)
        {
            string responseHtml = "<h1>Login()</h1>";

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

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