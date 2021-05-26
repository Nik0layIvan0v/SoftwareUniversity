using System.Collections.Generic;
using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using System.Diagnostics;
using System.Threading.Tasks;
using SUS.MvcFramework;

namespace MyFirstMvcApp
{
    public class Startup
    {
        public static async Task Main()
        {
            List<Route> routeTable = new List<Route>();

            routeTable.Add(new Route("/about", (request) => new HomeController().About(request)));
            routeTable.Add(new Route("/home", new HomeController().Index));
            routeTable.Add(new Route("/", new HomeController().Index));
            routeTable.Add(new Route("/favicon.ico", (httpRequest) => new StaticFilesController().Favicon(httpRequest)));
            routeTable.Add(new Route("/login", new UsersController().Login));
            routeTable.Add(new Route("/register", new UsersController().Register));

            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "http://localhost/");

            await Host.RunAsync(routeTable, 80);

        }
    }
}
