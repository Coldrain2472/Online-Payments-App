namespace OnlinePaymentsApp.Services.DTOs.Payment
{
    public class PaymentInfo
    {
        public int PaymentId { get; set; }

        public int FromAccountId { get; set; }

        public string ToAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public string Reason { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }
    }
}
