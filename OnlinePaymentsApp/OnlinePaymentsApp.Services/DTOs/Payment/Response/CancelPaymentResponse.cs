namespace OnlinePaymentsApp.Services.DTOs.Payment.Response
{
    public class CancelPaymentResponse : PaymentInfo
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
