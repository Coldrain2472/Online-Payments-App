using OnlinePaymentsApp.Services.DTOs.Account.Response;

namespace OnlinePaymentsApp.Services.Interfaces.Account
{
    public interface IAccountService
    {
        Task<GetAccountResponse> GetByIdAsync(int accountId);

        Task<GetAllAccountsResponse> GetAllAsync();
    }
}
