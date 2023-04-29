using Lab_2.Models;

namespace Lab__2_.Services
{
    public interface ICarService
    {
        RentalCarVm AddCarInCatalog(string ID, string Name, string Description, string Price, string Path);
    }
}
