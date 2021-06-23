using Musaca.Controllers;
using Musaca.Data;
using Musaca.Services.Users;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Musaca
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IDatabaseProvider, MusacaDbContext>();
            serviceCollection.Add<IUsersService, UsersService>();
        }

        public void Configure(ICollection<Route> routeTable)
        {

            new MusacaDbContext().Database.EnsureDeleted();
            new MusacaDbContext().Database.EnsureCreated();

            routeTable.Add(new Route("/", req => new HomeController().Index()));
        }
    }
}
