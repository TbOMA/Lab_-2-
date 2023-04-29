using Lab_2.Models;

namespace Lab__2_.Services
{
    public class CarSevice:ICarService
    {
        public RentalCarVm AddCarInCatalog(string ID,string Name,string Description,string Price,string Path)
        {
            var rental_car = new RentalCarVm
            {
                CarID = int.Parse(ID),
                CarName = Name,
                Description = Description,
                RentPrice = int.Parse(Price),
                CarImagePath = Path,
                IsAvailable = true
            };
            return rental_car;
        }
    }
}
