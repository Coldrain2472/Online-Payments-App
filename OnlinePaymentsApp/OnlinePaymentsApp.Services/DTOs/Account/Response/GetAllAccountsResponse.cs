namespace OnlinePaymentsApp.Services.DTOs.Account.Response
{
    public class GetAllAccountsResponse
    {
        public List<AccountInfo>? Accounts { get; set; }

        public int TotalCount { get; set; }
    }
}
