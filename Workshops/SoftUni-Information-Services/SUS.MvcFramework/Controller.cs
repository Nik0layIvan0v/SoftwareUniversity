using System;
using SUS.HTTP;
using System.Runtime.CompilerServices;
using System.Text;
using SUS.MvcFramework.ViewEngine;

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

            string layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");

            layout = layout.Replace("@RenderBody()", "___VIEW_GOES_HERE___");

            layout = this.ViewEngine.GetHtml(layout, viewModel);

            string combinationLayoutAndViewContent = layout.Replace("___VIEW_GOES_HERE___", viewContent);

            byte[] dataBytes = Encoding.UTF8.GetBytes(combinationLayoutAndViewContent);

            return new HttpResponse("text/html", dataBytes);
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
    }
}