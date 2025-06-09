using System.Data.SqlTypes;

namespace OnlinePaymentsApp.Repository.Interfaces.Payment
{
    public class PaymentFilter
    {
        public SqlDateTime? CreatedAt { get; set; }

        public SqlInt32? FromAccountId { get; set; }

        public SqlString? ToAccountNumber { get; set; }

        public SqlDecimal? Amount { get; set; }

        public SqlString? Status { get; set; }

        public SqlInt32? CreatedByUserId { get; set; }
    }
}
