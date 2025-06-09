using OnlinePaymentsApp.Repository.Base;

namespace OnlinePaymentsApp.Repository.Interfaces.UserAccount
{
    public interface IUserAccountRepository : IBaseRepository<Models.UserAccount, UserAccountFilter, UserAccountUpdate>
    {
        Task<bool> DeleteAsync(int userId, int accountId);
    }
}
