using MyFirstMvcApp.Controllers;
using SUS.MvcFramework;
using System.Collections.Generic;
using SUS.HTTP;

namespace MyFirstMvcApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices()
        {
            //TODO: implement ConfigureServices
        }

        public void Configure(ICollection<Route> routeTable)
        {
            routeTable.Add(new Route("/about", (request) => new HomeController().About(request)));
            routeTable.Add(new Route("/home", new HomeController().Index));
            routeTable.Add(new Route("/", new HomeController().Index));
            routeTable.Add(new Route("/login", new UsersController().Login));
            routeTable.Add(new Route("/register", new UsersController().Register));

            routeTable.Add(new Route("/favicon.ico", (httpRequest) =>
                new StaticFilesController().Favicon(httpRequest)));
            routeTable.Add(new Route("/wwwRoot/js/popper.min.js", (httpRequest) =>
                new StaticFilesController().Popper(httpRequest)));
            routeTable.Add(new Route("/wwwRoot/css/bootstrap.min.css", (httpRequest) =>
                new StaticFilesController().BootstrapCss(httpRequest)));
            routeTable.Add(new Route("/wwwRoot/js/jquery-3.4.1.min.js", (httpRequest) =>
                new StaticFilesController().Jquery(httpRequest)));
            routeTable.Add(new Route("/wwwRoot/js/bootstrap.min.js", (httpRequest) =>
                new StaticFilesController().BootstrapJs(httpRequest)));
        }
    }
}
