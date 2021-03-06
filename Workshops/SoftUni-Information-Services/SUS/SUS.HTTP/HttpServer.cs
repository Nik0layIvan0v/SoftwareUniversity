using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
        private readonly ICollection<Route> routeTable;

        public HttpServer(ICollection<Route> routes)
        {
            this.routeTable = routes;
        }

        public async Task StartAsync(int port = HttpConstants.DefaultPortNumber)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);

            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                _ = ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    int position = 0;

                    byte[] buffer = new byte[HttpConstants.DefaultBufferSize];

                    List<byte> processData = new List<byte>();

                    while (true)
                    {
                        int countOfReadBytes = await stream.ReadAsync(buffer.AsMemory(position, buffer.Length));

                        position += countOfReadBytes;

                        if (countOfReadBytes < buffer.Length)
                        {
                            var realBufferWithData = new byte[countOfReadBytes];

                            Array.Copy(buffer, realBufferWithData, countOfReadBytes);

                            processData.AddRange(realBufferWithData);
                            break;
                        }

                        processData.AddRange(buffer);
                    }

                    /* =====================================REQUEST============================================= */

                    string requestAsString = Encoding.UTF8.GetString(processData.ToArray());

                    HttpRequest request = new HttpRequest(requestAsString);

                    Console.WriteLine($"Request: Method => {request.Method} Path => {request.Path} Count of Headers: {request.Headers.Count}");

                    /* ====================================RESPONSE============================================= */

                    HttpResponse response;

                    Route route = this.routeTable.FirstOrDefault(x => string.Compare(x.Path, request.Path, StringComparison.OrdinalIgnoreCase) == 0
                                                                     && x.HttpMethod == request.Method);

                    if (route != null)
                    {
                        Func<HttpRequest, HttpResponse> action = route.Action;

                        response = action(request);
                    }
                    else
                    {
                        string layout = await System.IO.File.ReadAllTextAsync("Views/Shared/_Layout.cshtml");

                        string viewContent = await System.IO.File.ReadAllTextAsync("Views/Shared/ErrorView.cshtml");

                        string combinationLayoutAndViewContent = layout.Replace("@RenderBody()", viewContent);

                        byte[] dataBytes = Encoding.UTF8.GetBytes(combinationLayoutAndViewContent);

                        response = new HttpResponse("text/html", dataBytes, HttpStatusCode.NotFound);
                    }

                    Console.WriteLine($"Response: Status Code => {response.HttpStatusCode.ToString()} Path => {request.Path} Count of Headers: {request.Headers.Count}");

                    response.Headers.Add(new Header("Server", "MyCoolServer v1.0"));

                    Cookie sessionCookie = request.Cookies.FirstOrDefault(x => x.Name == HttpConstants.SessionCookieName);

                    if (sessionCookie != null)
                    {
                        ResponseCookie responseCookie = new ResponseCookie(sessionCookie.Name, sessionCookie.Value)
                        {
                            Path = "/",
                            HttpOnly = true,
                            MaxAge = 10000
                        };

                        response.Cookies.Add(responseCookie);
                    }

                    byte[] responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                    /*===> Send data through network stream <===*/

                    await stream.WriteAsync(responseHeaderBytes);

                    if (response.ResponseBody != null)
                    {
                        await stream.WriteAsync(response.ResponseBody);
                    }
                }

                tcpClient.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}