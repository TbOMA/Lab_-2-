using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models
{
    public class ClientVm
    {          
        [Key]
        public int Id { get; set; }
        public int ClientID { get; set; }
        public string ? Username { get; set; }
        public string ? PassportNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
