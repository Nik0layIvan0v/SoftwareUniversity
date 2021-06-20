using Git.Services.Repositories;
using Git.Services.Users;

namespace Git
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Collections.Generic;

    public class Startup : IMvcApplication
    {
        public void Configure(ICollection<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UserService>();
            serviceCollection.Add<IRepositoryService, RepositoryService>();
        }
    }
}
