using OnlinePaymentsApp.Services.DTOs.User.Response;

namespace OnlinePaymentsApp.Services.Interfaces.User
{
    public interface IUserService
    {
        Task<GetUserResponse> GetByIdAsync(int userId);

        Task<GetAllUsersResponse> GetAllAsync();
    }
}
