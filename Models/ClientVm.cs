using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_2.Models
{
    public class ClientVm
    {
        [Key]
        public int Id { get; set; }
       // public int ClientID { get; set; }   
        public string? Username { get; set; }
        public string? PassportNumber { get; set; }
        public decimal Balance { get; set; }
        public int? RentalFormVmId { get; set; }
        public RentalFormVm RentalFormVm { get; set; }
        public ICollection<RentalFormVm> ClientOrders { get; set; }
    }
}
