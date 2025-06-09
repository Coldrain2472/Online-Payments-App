using OnlinePaymentsApp.Repository.Base;

namespace OnlinePaymentsApp.Repository.Interfaces.Account
{
    public interface IAccountRepository : IBaseRepository<Models.Account, AccountFilter, AccountUpdate>
    {
        Task<Models.Account> GetByAccountNumberAsync(string accountNumber);
    }
}
