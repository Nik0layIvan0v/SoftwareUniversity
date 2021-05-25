using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyFirstMvcApp
{
    public class Startup
    {
        public static async Task Main()
        {
            IHttpServer sever = new HttpServer();

            sever.AddRoute("/about", (request) => new HomeController().About(request));

            sever.AddRoute("/home", new HomeController().Index);

            sever.AddRoute("/", new HomeController().Index);

            sever.AddRoute("/favicon.ico", (httpRequest) => new StaticFilesController().Favicon(httpRequest));

            sever.AddRoute("/login", new UsersController().Login);

            sever.AddRoute("/register", new UsersController().Register);

            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "http://localhost/");

            await sever.StartAsync(80);
        }
    }
}
