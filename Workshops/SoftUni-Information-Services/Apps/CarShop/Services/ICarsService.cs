using System.Collections.Generic;
using CarShop.ViewModels.Cars;

namespace CarShop.Services
{
    public interface ICarsService
    {
        List<CarViewModel> GetAllClientCarList(string ownerId);

        List<CarViewModel> GetAllMechanicCarList();

        void AddCar(AddCarInputModel model, string ownerId);
    }
}