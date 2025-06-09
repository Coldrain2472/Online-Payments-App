using System.Data.SqlTypes;

namespace OnlinePaymentsApp.Repository.Interfaces.UserAccount
{
    public class UserAccountFilter
    {
        public SqlInt32? UserId { get; set; }

        public SqlInt32? AccountId { get; set; }
    }
}
