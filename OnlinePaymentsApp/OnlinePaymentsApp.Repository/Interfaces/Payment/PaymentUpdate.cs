using System.Data.SqlTypes;

namespace OnlinePaymentsApp.Repository.Interfaces.Payment
{
    public class PaymentUpdate
    {
        public SqlDecimal? Amount { get; set; }

        public SqlString? Status { get; set; }
    }
}
