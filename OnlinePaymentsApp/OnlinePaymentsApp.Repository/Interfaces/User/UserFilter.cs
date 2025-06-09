using System.Data.SqlTypes;

namespace OnlinePaymentsApp.Repository.Interfaces.User
{
    public class UserFilter
    {
        public SqlString? Username { get; set; }
    }
}
