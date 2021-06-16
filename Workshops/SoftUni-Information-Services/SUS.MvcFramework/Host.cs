using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            IServiceCollection serviceCollection = new ServiceCollection();

            IMvcApplication application = Activator.CreateInstance<T>();

            await AutoMapStaticFiles(routes);

            application.ConfigureServices(serviceCollection);

            application.Configure(routes);

            await AutoRegisterRoutes(routes, application, serviceCollection);

            Console.WriteLine("All registered routes: ");

            foreach (var route in routes)
            {
                Console.WriteLine($"{route.HttpMethod} {route.Path}");
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

        /// <summary>
        /// Replace in Startup.cs: routeTable.Add(new Route("/login",new UsersController().LoginConfirmed, HttpMethod.Post)); to be automatic.
        /// </summary>
        /// <param name="routeTable">The route table</param>
        /// <param name="application">Current application</param>
        /// <returns>Task so CreateHostAsync to await the process</returns>
        private static async Task AutoRegisterRoutes(ICollection<Route> routeTable, IMvcApplication application, IServiceCollection serviceCollection)
        {
            await Task.Run(() =>
            {
                IEnumerable<Type> controllerTypes = application
                                                      .GetType().Assembly
                                                      .GetTypes()
                                                      .Where(type =>
                                                             type.IsClass &&
                                                             type.IsAbstract == false &&
                                                             type.IsSubclassOf(typeof(Controller)));

                foreach (Type controllerType in controllerTypes)
                {
                    //Console.WriteLine($"Controller Name: {controllerType.Name}");

                    MethodInfo[] controllerMethods = controllerType
                        .GetMethods()
                        .Where(methodInfo =>
                               methodInfo.IsPublic &&
                               methodInfo.IsStatic == false &&
                               methodInfo.DeclaringType == controllerType &&
                               methodInfo.IsAbstract == false &&
                               methodInfo.IsConstructor == false &&
                               methodInfo.IsSpecialName == false)
                        .ToArray();

                    //Console.WriteLine("Controller Methods: ");

                    foreach (var currentMethod in controllerMethods)
                    {
                        string controller = controllerType.Name.Replace("Controller", string.Empty);

                        //By default
                        string url = $"/{controller}/{currentMethod.Name}";

                        BaseHttpAttribute attribute = currentMethod
                                              .GetCustomAttributes()
                                              .FirstOrDefault
                                              (
                                                  a => a.GetType().IsSubclassOf(typeof(BaseHttpAttribute))
                                              ) as BaseHttpAttribute;

                        //If attribute have url example: [httpPost("/")] url will be changed to "/" instead of "/{controller}/{currentMethod.Name}"
                        if (!string.IsNullOrEmpty(attribute?.Url))
                        {
                            url = attribute.Url;
                        }

                        //By default controller method is GET if there is no attribute. Otherwise will be attribute method.
                        HttpMethod controllerMethod = attribute?.Method ?? HttpMethod.Get;

                        Func<HttpRequest, HttpResponse> controllerAction = (request) =>
                        {
                            var instance = serviceCollection.CreateInstance(controllerType) as Controller;

                            instance.HttpRequest = request;

                            HttpResponse httpResponse = currentMethod.Invoke(instance, Array.Empty<object>()) as HttpResponse;

                            return httpResponse;
                        };

                        routeTable.Add(new Route(url, controllerAction, controllerMethod));

                        //Console.WriteLine($"- {currentMethod.Name}() HttpMethod: {controllerMethod} URL: {url} ");
                    }
                }
            });
        }
    }
}