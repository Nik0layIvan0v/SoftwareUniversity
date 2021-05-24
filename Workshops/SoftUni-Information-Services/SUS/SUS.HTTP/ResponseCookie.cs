using System;
using System.Text;

namespace SUS.HTTP
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value)
            : base(name, value)
        {
        }

        public ResponseCookie(string cookieAsString) 
            : base(cookieAsString)
        {
        }

        public int MaxAge { get; set; }

        public string Path { get; set; } = "\\";

        public bool HttpOnly { get; set; }

        public string Domain { get; set; }

        public override string ToString()
        {
            StringBuilder cookieStringBuilder = new StringBuilder();

            cookieStringBuilder.Append($"{this.Name}={this.Value}; Path={this.Path};");

            if (this.MaxAge != 0)
            {
                cookieStringBuilder.Append($" Max-Age={this.MaxAge};");
            }

            if (this.HttpOnly == true)
            {
                cookieStringBuilder.Append($" HttpOnly;");
            }

            if (!string.IsNullOrWhiteSpace(this.Domain))
            {
                cookieStringBuilder.Append($" Domain={this.Domain}");
            }

            return cookieStringBuilder.ToString();
        }
    }
}