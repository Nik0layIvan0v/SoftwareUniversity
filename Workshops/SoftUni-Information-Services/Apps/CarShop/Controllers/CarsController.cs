using CarShop.Services;
using CarShop.ViewModels.Cars;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Text.RegularExpressions;
using static CarShop.Data.DataConstants.DataConstants;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private protected readonly CarService CarService;
        private protected readonly UsersService UsersService;

        public CarsController(CarService carService, UsersService usersService)
        {
            this.CarService = carService;
            this.UsersService = usersService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            string currentLoggedUserId = this.GetUserId();

            if (this.UsersService.IsUserMechanic(currentLoggedUserId))
            {
                return this.Error("Mechanics cannot add cars!");
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

            string currentLoggedUserId = this.GetUserId();

            if (this.UsersService.IsUserMechanic(currentLoggedUserId))
            {
                return this.Error("Mechanics cannot add cars!");
            }

            //Model – a string with min length 5 and max length 20 (required)
            if (carInputModel.Model.Length < CarModelMinLength || 
                carInputModel.Model.Length > DefaultMaxLength)
            {
                return this.Error("Car model name cannot be less than 5 characters or more than 20 characters long.");
            }

            //Year - a number (required)
            if (carInputModel.Year < CarYearMinValue || 
                carInputModel.Year > CarYearMaxValue )
            {
                return this.Error(
                    "Car year cannot be less than first worldwide produced car ever or more than current year");
            }

            //Image – a string (required)
            if (!Uri.IsWellFormedUriString(carInputModel.Image, UriKind.RelativeOrAbsolute))
            {
                return this.Error("Invalid car image!");
            }

            //Plate – a string – Must be a valid Plate number (2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters (required)
            if (!Regex.IsMatch(carInputModel.Plate, CarPlateNumberRegularExpression))
            {
                return this.Error("Invalid car plate number! Valid Plate number is: 2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters");
            }

            if (carInputModel.Plate.Length > CarPlateNumberMaxLength)
            {
                return this.Error($"Max characters for Plate number is {CarPlateNumberMaxLength}");
            }

            this.CarService.AddCar(carInputModel, currentLoggedUserId);

            return this.Redirect("/Cars/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You are not logged in!");
            }

            string currentLoggedUserId = this.GetUserId();

            if (!this.UsersService.IsUserMechanic(currentLoggedUserId))
            {
                var clientCarList = this.CarService.GetAllClientCarList(currentLoggedUserId).ToArray();

                return this.View(clientCarList);
            }

            var mechanicCarList = this.CarService.GetAllMechanicCarList().ToArray();

            return this.View(mechanicCarList);
        }
    }
}
