using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models
{
    public class CarsVm
    {
        [Key]
        public int Id { get; set; }
        //public int CarID { get; set; }
        public bool IsAvailable { get; set; }
        public decimal RentPrice { get; set; }
        public  bool IsDamaged { get; set; }
    }
}
