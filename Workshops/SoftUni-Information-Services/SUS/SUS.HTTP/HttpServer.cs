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
        private const int DefaultBufferSize = 4096;

        private const int DefaultPortNumber = 80;

        private readonly IDictionary<string, Func<HttpRequest, HttpResponse>> routeTable;

        public HttpServer()
        {
            this.routeTable = new Dictionary<string, Func<HttpRequest, HttpResponse>>();
        }

        public void AddRoute(string path, Func<HttpRequest, HttpResponse> action)
        {
            //TODO: Validate input;

            if (!this.routeTable.ContainsKey(path))
            {
                this.routeTable.Add(path, null);
            }

            this.routeTable[path] = action;
        }

        public async Task StartAsync(int port = DefaultPortNumber)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, port);

            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            using (NetworkStream stream = tcpClient.GetStream())
            {
                int position = 0;

                byte[] buffer = new byte[DefaultBufferSize];

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
            }
        }
    }
}