using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        /// <summary>
        /// Stores all public sessions ids from different browsers /clients/ 
        /// </summary>
        public static IDictionary<string, Dictionary<string, string>> Sessions =
            new Dictionary<string, Dictionary<string, string>>(StringComparer.InvariantCultureIgnoreCase);

        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            this.SessionData = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            string[] lines = requestString
                .Split(HttpConstants.HttpNewLine, StringSplitOptions.None)
                .ToArray();

            //GET /Home/About HTTP/1.1
            string headerLine = lines[0];

            string[] headerLineParts = headerLine.Split(' ').ToArray();

            this.Method = Enum.Parse<HttpMethod>(headerLineParts[0], true);

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

            if (this.Headers.Any(header => header.Name == HttpConstants.RequestCookieHeader))
            {
                string cookiesAsString =
                    this.Headers.FirstOrDefault(x => x.Name == HttpConstants.RequestCookieHeader)?.Value;

                string[] cookies = cookiesAsString
                    ?.Split("; ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                if (cookies != null)
                {
                    foreach (var currentCookieAsString in cookies)
                    {
                        this.Cookies.Add(new Cookie(currentCookieAsString));
                    }
                }
            }

            Cookie sessionCookie = this.Cookies
                .FirstOrDefault(x => x.Name == HttpConstants.SessionCookieName);

            if (sessionCookie == null)
            {
                string newSessionId = Guid.NewGuid().ToString();

                this.SessionData = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

                Sessions.Add(newSessionId, this.SessionData);

                this.Cookies.Add(new Cookie(HttpConstants.SessionCookieName, newSessionId));
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                //If we stop server and then start it again and browser send last cookie.
                this.SessionData = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

                Sessions.Add(sessionCookie.Value, this.SessionData);
            }
            else
            {
                this.SessionData = Sessions[sessionCookie.Value];
            }

            this.RequestBody = bodyBuilder.ToString().TrimEnd('\n', '\r');

            if (!string.IsNullOrEmpty(this.RequestBody))
            {
                ParseFormDataFromBody();
            }
        }

        private void ParseFormDataFromBody()
        {
            //TODO: May be validate user input from html form and escape special characters like = &...
            //RAW DATA: firstName=testName&lastName=testName

            //==after split==> [0] => firstName=testName [1] => lastName=testName ...and so on
            string[] bodyParameters = this.RequestBody.Split("&", StringSplitOptions.RemoveEmptyEntries);

            foreach (var parameter in bodyParameters)
            {
                //==after split==> [0] => firstName [1] => testName ...and so on
                string[] parameterParts = parameter.Split("=", 2);

                string key = parameterParts[0];
                string value = WebUtility.UrlDecode(parameterParts[1]);

                if (!this.FormData.ContainsKey(key))
                {
                    this.FormData.Add(key, string.Empty);
                }

                this.FormData[key] = value;
            }
        }

        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        /// <summary>
        /// Holds sensitive information behind cookie like username/userid or else.
        /// </summary>
        public Dictionary<string, string> SessionData { get; set; }

        public IDictionary<string, string> FormData { get; set; }

        public string RequestBody { get; set; }
    }
}
