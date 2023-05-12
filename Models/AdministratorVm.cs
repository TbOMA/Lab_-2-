using System.ComponentModel.DataAnnotations;

namespace Lab_2.Models
{
    public class AdministratorVm 
    {
        [Key]
        public int AdministratorID { get; set; }
        public string ? UserName { get; set; }
        public string ? Password { get; set; }
    }
}
