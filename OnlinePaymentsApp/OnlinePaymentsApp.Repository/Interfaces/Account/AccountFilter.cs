using System.Data.SqlTypes;

namespace OnlinePaymentsApp.Repository.Interfaces.Account
{
    public class AccountFilter
    {
        public SqlString? AccountNumber { get; set; }
    }
}
