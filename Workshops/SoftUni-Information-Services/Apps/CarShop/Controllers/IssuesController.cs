using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            return this.View();
        }

        [HttpPost("/Issues/Add")]
        public HttpResponse AddCar()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            return this.Redirect("/");
        }
    }
}