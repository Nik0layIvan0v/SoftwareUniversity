using System.Collections.Generic;

namespace SUS.MvcFramework
{
    public interface IMvcApplication
    {
        void ConfigureServices();

        void Configure(ICollection<Route> routeTable);
    }
}