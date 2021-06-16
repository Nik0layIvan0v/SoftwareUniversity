using System.Collections.Generic;
using SUS.HTTP;

namespace SUS.MvcFramework
{
    public interface IMvcApplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);

        void Configure(ICollection<Route> routeTable);
    }
}