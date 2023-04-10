using System.Collections.Generic;
namespace Lab_2.Models
{
    public class CarsVm
    {
        public int CarID { get; set; }
        public bool IsAvailable { get; set; }
        public decimal RentPrice { get; set; }
        public  bool IsDamaged { get; set; }
        public CarsVm() { }
        public CarsVm(int carID, bool isAvailable, decimal rentPrice)
        {
            CarID = carID;
            IsAvailable = isAvailable;
            RentPrice = rentPrice;
        }
    }
}
