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
            routeTable.Add(new Route("/login",new UsersController().LoginConfirmed, HttpMethod.Post));
            routeTable.Add(new Route("/register", new UsersController().Register));
        }
    }
}
