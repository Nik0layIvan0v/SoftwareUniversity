using System.Threading.Tasks;
using SUS.HTTP;

namespace MyFirstMvcApp
{
    public class Startup
    {
        public static async Task Main()
        {
            IHttpServer sever = new HttpServer();

            //1. Variant
            sever.AddRoute("/about", (request) =>
            {
                return new HttpResponse();
            });

            //2. Variant
            sever.AddRoute("/home", HomePage);

            await sever.StartAsync(80);
        }

        public static HttpResponse HomePage(HttpRequest request)
        {
            return new HttpResponse();
        }
    }
}
