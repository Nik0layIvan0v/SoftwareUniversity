using System.Collections.Generic;
using CarShop.ViewModels.Cars;

namespace CarShop.Services
{
    public interface ICarsService
    {
        List<CarViewModel> GetAllClientCarList(); // model?

        List<CarViewModel> GetAllMechanicCarList(); // model?

        void AddCar(AddCarInputModel model); // model?
    }
}