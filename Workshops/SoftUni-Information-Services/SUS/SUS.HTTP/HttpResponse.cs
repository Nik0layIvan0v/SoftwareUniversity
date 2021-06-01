using System;
using System.Collections.Generic;
using System.Text;

namespace SUS.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.HttpStatusCode = statusCode;
            this.Cookies = new List<Cookie>();
            this.Headers = new List<Header>();
            this.ResponseBody = new byte[0];
        }

        public HttpResponse(string contentType, byte[] responseBodyBytes, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            if (responseBodyBytes == null)
            {
                throw new ArgumentNullException((nameof(responseBodyBytes)));
            }

            this.Headers = new List<Header>
            {
                new Header("Content-Type", contentType),
                new Header("Content-Length", $"{responseBodyBytes.Length.ToString()}")
            };

            this.Cookies = new List<Cookie>();
            this.ResponseBody = responseBodyBytes;
            this.HttpStatusCode = statusCode;

        }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public byte[] ResponseBody { get; set; }

        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();
            responseBuilder.Append($"HTTP/1.1 {(int)this.HttpStatusCode} {this.HttpStatusCode.ToString()}{HttpConstants.HttpNewLine}");

            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header.ToString());
                responseBuilder.Append(HttpConstants.HttpNewLine);
            }

            foreach (var cookie in Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString());
                responseBuilder.Append(HttpConstants.HttpNewLine);
            }

            responseBuilder.Append(HttpConstants.HttpNewLine);

            return responseBuilder.ToString();
        }
    }
}