using CarShop.Data;
using CarShop.Services;

using Microsoft.EntityFrameworkCore;

using SUS.HTTP;
using SUS.MvcFramework;

using System.Collections.Generic;

namespace CarShop
{
    public class Startup : IMvcApplication
    {

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
        }

        public void Configure(ICollection<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
