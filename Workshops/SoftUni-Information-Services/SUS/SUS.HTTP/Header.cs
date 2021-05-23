using System;
using System.Linq;

namespace SUS.HTTP
{
    public class Header
    {
        public Header(string headerLine)
        {
            string[] headerParts = headerLine
                .Split(": " , 2, StringSplitOptions.None)
                .ToArray();

            this.Name = headerParts[0];

            this.Value = headerParts[1];
        }
        public string Name { get; set; }

        public string Value { get; set; }
    }
}