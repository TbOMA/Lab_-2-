using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_2.Models
{
    public class RentalFormVm
    {
    [Key]
    public int RentalFormID { get; set; }
    public bool IsApproved { get; set; }

    public int CarID { get; set; }

    public CarsVm Car { get; set; }

    public int ClientID { get; set; }

    public ClientVm Client { get; set; }

    public string RejectionReason { get; set; }
    public bool IsConsidered { get; set; }
    }

}
