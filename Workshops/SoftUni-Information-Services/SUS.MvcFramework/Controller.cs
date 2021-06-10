using SUS.HTTP;
using SUS.MvcFramework.ViewEngine;
using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        protected Controller()
        {
            this.ViewEngine = new SusViewEngine();
        }

        public SusViewEngine ViewEngine { get; set; }

        public HttpRequest HttpRequest { get; set; }

        public HttpResponse View(object viewModel = null, [CallerMemberName] string actionName = null)
        {
            string controllerName = this.GetType().Name.Replace("Controller", string.Empty);

            string viewContent = System.IO.File.ReadAllText($"Views/{controllerName}/{actionName}.cshtml");

            viewContent = this.ViewEngine.GetHtml(viewContent, viewModel);

            string combinationLayoutAndViewContent = this.PutViewInLayout(viewContent, viewModel);

            byte[] dataBytes = Encoding.UTF8.GetBytes(combinationLayoutAndViewContent);

            return new HttpResponse("text/html", dataBytes);
        }

        private string PutViewInLayout(string viewContent, object viewModel = null)
        {
            string layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");

            layout = layout.Replace("@RenderBody()", "___VIEW_GOES_HERE___");

            layout = this.ViewEngine.GetHtml(layout, viewModel);

            string combinationLayoutAndViewContent = layout.Replace("___VIEW_GOES_HERE___", viewContent);

            return combinationLayoutAndViewContent;
        }
        public HttpResponse FileResponse(string filePath, string contentType)
        {
            HttpResponse response = new HttpResponse(contentType, Array.Empty<byte>());

            if (System.IO.File.Exists(filePath))
            {
                byte[] data = System.IO.File.ReadAllBytes(filePath);
                response = new HttpResponse(contentType, data);
            }

            return response;
        }

        public HttpResponse Redirect(string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);

            response.Headers.Add(new Header("Location", url));

            return response;
        }

        /// <summary>
        /// Helper method for input validations of all derived controllers
        /// </summary>
        /// <param name="message">The message to show</param>
        /// <returns>HttpResponse with visual message for UI (for example on the layout)</returns>
        public HttpResponse Error(string message)
        {
            string alertError = $@"<div class=""alert alert-danger"" role=""alert"">{message}</div>";

            string combinationLayoutAndViewContent = this.PutViewInLayout(alertError);

            byte[] dataBytes = Encoding.UTF8.GetBytes(combinationLayoutAndViewContent);

            return new HttpResponse("text/html", dataBytes, HttpStatusCode.Unauthorized);
        }
    }
}