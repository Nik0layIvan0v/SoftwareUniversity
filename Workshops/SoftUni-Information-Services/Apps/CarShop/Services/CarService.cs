using System.Collections.Generic;
using System.Linq;
using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels.Cars;

namespace CarShop.Services
{
    public class CarService : ICarsService
    {
        private protected readonly ApplicationDbContext DbContext;

        public CarService(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public List<CarViewModel> GetAllClientCarList(string ownerId)
        {
            return this.DbContext.Cars
                .Where(o => o.OwnerId == ownerId)
                .Select(car => new CarViewModel
                {
                    Id = car.Id,
                    Image = car.PictureUrl, 
                    CarModel = car.Model,
                    Year = car.Year,
                    PlateNumber = car.PlateNumber,
                    RemainingIssues = car.Issues.Count(x => x.IsFixed == false),
                    FixedIssues = car.Issues.Count(x => x.IsFixed == true)

                }).ToList();
        }

        public List<CarViewModel> GetAllMechanicCarList()
        {
            return this.DbContext.Cars
                .Where(c => c.Issues.Any(x => x.IsFixed == false))
                .Select(car => new CarViewModel
                {
                    Id = car.Id,
                    Image = car.PictureUrl,
                    CarModel = car.Model,
                    Year = car.Year,
                    PlateNumber = car.PlateNumber,
                    RemainingIssues = car.Issues.Count(x => x.IsFixed == false),
                    FixedIssues = car.Issues.Count(x => x.IsFixed == true)

                }).ToList();
        }

        public void AddCar(AddCarInputModel model, string ownerId)
        {
            Car car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.Plate,
                OwnerId = ownerId
            };

            this.DbContext.Cars.Add(car);
            this.DbContext.SaveChanges();
        }
    }
}
