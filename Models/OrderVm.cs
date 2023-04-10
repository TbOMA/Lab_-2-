using System.Collections.Generic;
using System;
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
        public OrderVm() { }
        public OrderVm(int carID, int clientID, string fullName, string passportNumber, int rentalTime,
            bool isApproved, string rejectionReason, decimal totalAmount, bool isPaid, bool isConsidered) :base(isApproved, rejectionReason, totalAmount, isPaid, isConsidered)
        {
            CarID = carID;
            RentalTime = rentalTime;
            ClientID = clientID;
            FullName = fullName;
            PassportNumber = passportNumber;
        }

    }
}
