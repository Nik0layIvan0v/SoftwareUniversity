using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SUS.HTTP;

namespace MyFirstMvcApp
{
    public class Startup
    {
        public static async Task Main()
        {
            IHttpServer sever = new HttpServer();

            //1. Variant
            sever.AddRoute("/about", (request) =>
            {
                string responseHtml = "<h1>sever.AddRoute(\" /about\", (request) =></h1>";

                byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

                HttpResponse response = new HttpResponse("text/html", responseBytes);

                return response;
            });

            //2.Variant
            sever.AddRoute("/home", HomePage);

            sever.AddRoute("/", HomePage);

            sever.AddRoute("/favicon.ico", (request) =>
            {
                byte[] faviconBytes = File.ReadAllBytes(@"wwwRoot/favicon.ico");

                HttpResponse response = new HttpResponse("image/vnd.microsoft.icon", faviconBytes);

                return response;
            });

            await sever.StartAsync(80);
        }

        public static HttpResponse HomePage(HttpRequest request)
        {
            string responseHtml = "<h1>HomePage()</h1>";

            byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

            HttpResponse response = new HttpResponse("text/html", responseBytes);

            return response;
        }
    }
}
