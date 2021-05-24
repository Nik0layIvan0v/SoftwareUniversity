using System.Text;
using SUS.HTTP;

namespace MyFirstMvcApp.Controllers
{
    public class HomeController
    {
        public HttpResponse Index(HttpRequest request)
        {
            string responseHtml = "<h1>HomePage()</h1>";

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }

        public HttpResponse About(HttpRequest request)
        {
            string responseHtml = "<h1>sever.AddRoute(\" /about\", (request) =></h1>";

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }
    }
}