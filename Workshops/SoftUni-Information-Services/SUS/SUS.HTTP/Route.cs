using System;

namespace SUS.HTTP
{
    public class Route
    {
        public Route(string path, Func<HttpRequest, HttpResponse> action, HttpMethod httpMethod = HttpMethod.Get)
        {
            this.Path = path;
            this.Action = action;
            this.HttpMethod = httpMethod;
        }

        public string Path { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public Func<HttpRequest, HttpResponse> Action { get; set; }
    }
}