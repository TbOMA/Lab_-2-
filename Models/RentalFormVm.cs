using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_2.Models
{
    public class RentalFormVm
    {
        [Key]
        public int RentalFormID { get; set; }
        public bool IsApproved { get; set; }

        [ForeignKey("Car")]
        public CarsVm Car { get; set; }

        [ForeignKey("Client")]
        public ClientVm Client { get; set; }

        public string RejectionReason { get; set; }
        public bool IsConsidered { get; set; }
    }

}
