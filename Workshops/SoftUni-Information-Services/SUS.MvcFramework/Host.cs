using System.Collections.Generic;
using SUS.HTTP;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(List<Route> routes, int port = 80)
        {
            IHttpServer sever = new HttpServer(routes);

            await sever.StartAsync(port);
        }
    }
}