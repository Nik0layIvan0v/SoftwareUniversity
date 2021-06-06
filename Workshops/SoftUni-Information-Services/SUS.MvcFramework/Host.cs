using System;
using System.Collections.Generic;
using SUS.HTTP;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync<T>(int port = HttpConstants.DefaultPortNumber)
             where T : class, IMvcApplication
        {
            ICollection<Route> routes = new List<Route>();

            IMvcApplication application = Activator.CreateInstance<T>();

            application.ConfigureServices();

            application.Configure(routes);

            IHttpServer sever = new HttpServer(routes);

            await sever.StartAsync(port);
        }
    }
}