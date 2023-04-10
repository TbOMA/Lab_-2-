using System;
using System.Collections.Generic;
namespace Lab_2.Models
{
    public class ClientVm
    {   
        public static List<ClientVm> clientlist = new List<ClientVm> { };
        public int ClientID { get; set; }
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public decimal Balance { get; set; }
        public ClientVm() { }
        public ClientVm(int clientID, string fullName, string passportNumber, decimal balance)
        {
            ClientID = clientID;
            FullName = fullName;
            PassportNumber = passportNumber;
            Balance = balance;
        }
    }
}
