using OnlinePaymentsApp.Services.DTOs.UserAccount.Request;
using OnlinePaymentsApp.Services.DTOs.UserAccount.Response;

namespace OnlinePaymentsApp.Services.Interfaces.UserAccount
{
    public interface IUserAccountService
    {
        Task<AddUserAccountResponse> AddUserAccountAsync(AddUserAccountRequest request);

        Task<RemoveUserAccountResponse> RemoveUserAccountAsync(RemoveUserAccountRequest request); // maybe implement in the future -> users to be able to remove accounts?

        Task<GetAllUserAccountsResponse> GetAllUserAccountsAsync(GetAllUserAccountsRequest request);
    }
}
