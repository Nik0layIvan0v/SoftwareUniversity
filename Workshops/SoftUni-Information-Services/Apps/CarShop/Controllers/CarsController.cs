using CarShop.ViewModels.Cars;
using SUS.HTTP;
using SUS.MvcFramework;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            return this.View();
        }

        [HttpPost("/Cars/Add")]
        public HttpResponse AddCar(AddCarInputModel carInputModel)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            return this.Redirect("/Cars/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            return this.View();
        }
    }
}
