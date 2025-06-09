using OnlinePaymentsApp.Repository.Base;

namespace OnlinePaymentsApp.Repository.Interfaces.Payment
{
    public interface IPaymentRepository : IBaseRepository<Models.Payment, PaymentFilter, PaymentUpdate>
    {
    }
}
