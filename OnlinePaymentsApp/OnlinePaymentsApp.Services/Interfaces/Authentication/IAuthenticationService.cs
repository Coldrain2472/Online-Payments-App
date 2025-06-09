using OnlinePaymentsApp.Services.DTOs.Authentication.Request;
using OnlinePaymentsApp.Services.DTOs.Authentication.Response;

namespace OnlinePaymentsApp.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
