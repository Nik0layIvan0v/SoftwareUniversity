using MyFirstMvcApp.Controllers;
using MyFirstMvcApp.Data;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFirstMvcApp.Services;

namespace MyFirstMvcApp
{
    public class Startup : IMvcApplication
    {
        /// <summary>
        /// Here register all dependencies of the application. Interface => Concrete instance!
        /// </summary>
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            //serviceCollection.Add<ApplicationDbContext, ApplicationDbContext>();
        }

        public void Configure(ICollection<Route> routeTable) 
        {
            new ApplicationDbContext().Database.Migrate();

            //To be sure that even if there is no [HttpGet("/")] the route "/" with GET method to be registered!
            //For manual configure some route add what you need and use id in register method!

            //Syntax: routeTable.Add(new Route("/", request => new HomeController({Servixe/Dbcontext}).Index(), HttpMethod.Get));
        }
    }
}
