namespace Lab_2.Models
{
    public sealed class RentalCarVm : CarsVm
    {
        new public bool IsDamaged { get; set; }
        public static string DamageDescription { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
    }
}
