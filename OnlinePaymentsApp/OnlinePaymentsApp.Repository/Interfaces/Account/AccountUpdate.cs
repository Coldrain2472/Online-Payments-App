using System.Data.SqlTypes;

namespace OnlinePaymentsApp.Repository.Interfaces.Account
{
    public class AccountUpdate
    {
        public SqlDecimal? Balance { get; set; }
    }
}
