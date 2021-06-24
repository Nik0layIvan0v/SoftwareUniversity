using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;
using BattleCards.Data;
using BattleCards.Services;
using Microsoft.EntityFrameworkCore;

namespace BattleCards
{
    public class Startup : IMvcApplication
    {
        public void Configure(ICollection<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<ApplicationDbContext>();
            serviceCollection.Add<IUserService, UsersService>();
        }
    }
}
