using OnlinePaymentsApp.Services.DTOs.Account;

namespace OnlinePaymentsApp.Services.DTOs.UserAccount.Response
{
    public class GetAllUserAccountsResponse
    {
        public int UserId { get; set; }

        public List<AccountInfo>? Accounts { get; set; }

        public int TotalCount { get; set; }
    }
}
