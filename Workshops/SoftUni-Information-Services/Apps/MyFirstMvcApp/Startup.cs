using SUS.HTTP;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyFirstMvcApp.Controllers;

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

            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "http://localhost/");

            await sever.StartAsync(80);
        }
    }
}
