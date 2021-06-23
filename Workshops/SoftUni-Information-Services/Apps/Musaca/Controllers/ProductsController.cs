using SUS.HTTP;
using SUS.MvcFramework;

namespace Musaca.Controllers
{
    public class ProductsController : Controller
    {
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/users/login");
            }

            return this.View();
        }
    }
}