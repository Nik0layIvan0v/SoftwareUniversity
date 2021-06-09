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
            //To be sure that even if there is no [HttpGet("/")] the route "/" with GET method to be registered!
            routeTable.Add(new Route("/", request => new HomeController().Index()));
        }
    }
}
