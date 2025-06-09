namespace OnlinePaymentsApp.Web.Models.Account
{
    public class AccountListViewModel
    {
        public List<AccountViewModel> Accounts { get; set; }

        public int TotalCount { get; set; }
    }

    public class AccountViewModel
    {
        public int AccountId { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }
    }
}
