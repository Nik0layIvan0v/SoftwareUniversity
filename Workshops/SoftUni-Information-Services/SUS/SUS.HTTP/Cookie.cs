using System;
using System.Linq;

namespace SUS.HTTP
{
    public class Cookie
    {
        public Cookie(string cookieAsString)
        {
            //Parse logic
            string[] cookieParts = cookieAsString
                .Split('=', 2, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            this.Name = cookieParts[0];

            this.Value = cookieParts[1];
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}