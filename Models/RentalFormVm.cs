namespace Lab_2.Models
{
    public class RentalFormVm
    {
        public int RentalFormID { get; set; }
        public bool IsApproved { get; set; }
        public string RejectionReason { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public bool IsConsidered { get; set; }
        public RentalFormVm() { }
        public RentalFormVm(bool isApproved, string rejectionReason, decimal totalAmount, bool isPaid, bool isConsidered)
        {
            IsApproved = isApproved;
            RejectionReason = rejectionReason;
            TotalAmount = totalAmount;
            IsPaid = isPaid;
            IsConsidered = isConsidered;
        }
    }

}
