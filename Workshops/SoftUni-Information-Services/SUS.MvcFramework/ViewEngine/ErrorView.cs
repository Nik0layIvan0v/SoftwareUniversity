using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUS.MvcFramework.ViewEngine
{
    public class ErrorView : IView
    {
        private readonly IEnumerable<string> errors;
        private readonly string cSharpCode;

        public ErrorView(IEnumerable<string> errors, string csharpCode)
        {
            this.cSharpCode = csharpCode;
            this.errors = errors;
        }

        public string GenerateHtml(object viewModel, string user, string userRole, string username)
        {
            var html = new StringBuilder();

            html.AppendLine($"<h1>View compile {this.errors.Count()} errors: </h1>");

            html.AppendLine("<ol>");

            foreach (var currentError in this.errors)
            {
                html.AppendLine($"<li>{currentError}</li>");
            }

            html.AppendLine("</ol>");

            html.AppendLine($"<pre>{cSharpCode}</pre>");

            return html.ToString();
        }
    }
}