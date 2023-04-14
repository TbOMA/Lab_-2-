using System.Collections.Generic;
namespace Lab_2.Models
{
    public sealed class RentalCarVm : CarsVm
    {
        new public bool IsDamaged { get; set; }
        public static string DamageDescription { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
        public RentalCarVm() { }
        public RentalCarVm(int carID, string carName, string description,  bool isAvailable, decimal rentPrice) : base(carID, isAvailable, rentPrice)
        {
            CarName = carName;
            Description = description;
           
        }
    }
}
