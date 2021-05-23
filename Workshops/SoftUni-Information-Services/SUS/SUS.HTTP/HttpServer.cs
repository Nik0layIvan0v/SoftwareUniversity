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
        private readonly IDictionary<string, Func<HttpRequest, HttpResponse>> routeTable;

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

                    string requestAsString = Encoding.UTF8.GetString(processData.ToArray());

                    Console.WriteLine(requestAsString);

                    HttpRequest request = new HttpRequest(requestAsString);

                    var responseHtml = "<h1>Welcome</h1>";

                    var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

                    var responseHttp = "http/1.1 200 OK" + HttpConstants.HttpNewLine
                                                         + "Server: Sus server v1.0." + HttpConstants.HttpNewLine
                                                         + "Content-Type: text/html" + HttpConstants.HttpNewLine
                                                         + "Content-Length: " + responseBodyBytes.Length + HttpConstants.HttpNewLine + HttpConstants.HttpNewLine;

                    var responseHeaderBytes = Encoding.UTF8.GetBytes(responseHttp);

                    await stream.WriteAsync(responseHeaderBytes);

                    await stream.WriteAsync(responseBodyBytes);
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