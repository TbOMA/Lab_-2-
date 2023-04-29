
using System.Collections.Generic;

namespace Lab_2.Models
{
    public class ClientVm
    {   
        public static List<ClientVm> clientlist = new List<ClientVm> { };
        public int ClientID { get; set; }
        public string UserName { get; set; }
        public string PassportNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
