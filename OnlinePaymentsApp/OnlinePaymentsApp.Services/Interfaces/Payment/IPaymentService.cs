using OnlinePaymentsApp.Services.DTOs.Payment.Request;
using OnlinePaymentsApp.Services.DTOs.Payment.Response;

namespace OnlinePaymentsApp.Services.Interfaces.Payment
{
    public interface IPaymentService
    {
        Task<GetPaymentResponse> GetByIdAsync(int paymentId);

        Task<GetAllPaymentsResponse> GetAllChronologicallyAsync(int userId);

        Task<GetAllPaymentsResponse> GetAllByStatusAsync(int userId);

        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest request);

        Task<SendPaymentResponse> SendAsync(SendPaymentRequest request);

        Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request);
    }
}
