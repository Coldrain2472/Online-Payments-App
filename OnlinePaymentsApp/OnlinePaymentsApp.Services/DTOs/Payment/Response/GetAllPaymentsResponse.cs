namespace OnlinePaymentsApp.Services.DTOs.Payment.Response
{
    public class GetAllPaymentsResponse
    {
        public List<PaymentInfo>? Payments { get; set; }

        public int TotalCount { get; set; }
    }
}
