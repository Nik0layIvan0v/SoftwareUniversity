using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            string[] lines = requestString
                .Split(HttpConstants.HttpNewLine, StringSplitOptions.None)
                .ToArray();

            //GET /Home/About HTTP/1.1
            string headerLine = lines[0];

            string[] headerLineParts = headerLine.Split(' ').ToArray();

            this.Method = headerLineParts[0];
            this.Path = headerLineParts[1];

            StringBuilder bodyBuilder = new StringBuilder();

            bool isInHeaders = true;

            for (int index = 1; index < lines.Length; index++)
            {
                string line = lines[index];

                if (string.IsNullOrWhiteSpace(line))
                {
                    isInHeaders = false;
                    continue;
                }

                if (isInHeaders)
                {
                    //Read Headers!
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    //Read Body!
                    bodyBuilder.Append(line + HttpConstants.HttpNewLine);
                }
            }

            this.RequestBody = bodyBuilder.ToString();
        }

        public string Path { get; set; }

        public string Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public string RequestBody { get; set; }
    }
}
