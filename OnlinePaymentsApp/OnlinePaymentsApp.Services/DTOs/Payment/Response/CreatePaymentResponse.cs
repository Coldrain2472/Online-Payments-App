namespace OnlinePaymentsApp.Services.DTOs.Payment.Response
{
    public class CreatePaymentResponse : PaymentInfo
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
