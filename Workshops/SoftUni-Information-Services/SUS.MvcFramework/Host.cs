using System;
using System.Collections.Generic;
using System.IO;
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

            await AutoMapStaticFiles(routes);

            application.ConfigureServices();

            application.Configure(routes);

            Console.WriteLine("All Registered Routes:");

            foreach (var curRoute in routes)
            {
                Console.WriteLine($"{curRoute.HttpMethod} {curRoute.Path}");
            }

            IHttpServer sever = new HttpServer(routes);

            await sever.StartAsync(port);
        }

        private static async Task AutoMapStaticFiles(ICollection<Route> routeTable)
        {
            await Task.Run(() =>
            {
                string[] staticFiles = Directory.GetFiles("wwwRoot", "*", SearchOption.AllDirectories);

                foreach (var currentFile in staticFiles)
                {
                    string path = currentFile
                        .Replace("wwwRoot", string.Empty)
                        .Replace("\\", "/");

                    routeTable.Add(new Route(path, request =>
                    {
                        byte[] fileContent = File.ReadAllBytes(currentFile);

                        string fileInfo = new FileInfo(currentFile).Extension;

                        string mimeType = fileInfo switch
                        {
                            ".txt" => "text/plain",
                            ".js" => "text/javascript",
                            ".css" => "text/css",
                            ".jpg" => "image/jpg",
                            ".jpeg" => "image/jpg",
                            ".png" => "image/png",
                            ".gif" => "image/gif",
                            ".ico" => "image/vnd.microsoft.icon",
                            ".html" => "text/html",
                            _ => "text/plain",
                        };

                        return new HttpResponse(mimeType, fileContent, HttpStatusCode.Ok);
                    }));
                }
            });
        }
    }
}