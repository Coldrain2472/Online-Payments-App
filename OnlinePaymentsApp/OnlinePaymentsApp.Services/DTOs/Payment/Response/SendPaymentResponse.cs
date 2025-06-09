namespace OnlinePaymentsApp.Services.DTOs.Payment.Response
{
    public class SendPaymentResponse : PaymentInfo
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
