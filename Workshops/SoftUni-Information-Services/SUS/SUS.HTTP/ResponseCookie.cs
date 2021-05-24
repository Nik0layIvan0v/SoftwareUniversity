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

        public string Path { get; set; }

        public bool HttpOnly { get; set; }

        public string Domain { get; set; }
    }
}