using Lab_2.Models;
using System.Collections.Generic;

namespace Lab__2_.Services
{
    public interface ICarService
    {
        RentalCarVm AddCarInCatalog(string ID, string Name, string Description, string Price, string Path);
        void Create(RentalCarVm rentalCarVm);
        bool Delete(int id);
        List<RentalCarVm> GetAll();
        RentalCarVm GetById(int id);
        bool UpDate(RentalCarVm rentalCarVm);
    }
}
