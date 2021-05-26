using System.Collections.Generic;
using SUS.HTTP;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(List<Route> routes, int port)
        {
            IHttpServer sever = new HttpServer();

            foreach (var route in routes)
            {
                sever.AddRoute(route.Path, route.Action);
            }

            await sever.StartAsync(port);
        }
    }
}