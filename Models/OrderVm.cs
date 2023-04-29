using System.Collections.Generic;

namespace Lab_2.Models
{
    public class OrderVm : RentalFormVm
    {
        public static List<OrderVm> ? orders = new List<OrderVm> { };
        public int CarID { get; set; }
        public int RentalTime { get; set; }
        public int ClientID { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
    }
}
