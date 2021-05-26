using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
        private Dictionary<string, Func<HttpRequest, HttpResponse>> routeTable;

        public HttpServer()
        {
            this.routeTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();
        }

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            //TODO: Validate input for null values!;

            if (!this.routeTable.ContainsKey(path))
            {
                this.routeTable.Add(path, null);
            }

            this.routeTable[path] = action;
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
                        int countOfReadBytes = await stream.ReadAsync(buffer, position, buffer.Length);

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

                    Console.WriteLine($"{request.Method} => {request.Path} => {request.Headers.Count} Headers");

                    /* ====================================RESPONSE============================================= */

                    HttpResponse response;

                    if (this.routeTable.ContainsKey(request.Path))
                    {
                        Func<HttpRequest, HttpResponse> action = this.routeTable[request.Path];

                        response = action(request);
                    }
                    else
                    {
                        string responseHtml = "<h1>404 NOT FOUND (this.routeTable.ContainsKey(request.Path) returned false!</h1>";

                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseHtml);

                        response = new HttpResponse("text/html", responseBytes, HttpStatusCode.NotFound);
                        //404 not found
                    }

                    response.Cookies.Add(new Cookie("SID", Guid.NewGuid().ToString()));

                    byte[] responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                    /*===> Send data through network stream <===*/

                    await stream.WriteAsync(responseHeaderBytes);

                    await stream.WriteAsync(response.ResponseBody);
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