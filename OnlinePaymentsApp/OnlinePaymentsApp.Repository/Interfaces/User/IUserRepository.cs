using OnlinePaymentsApp.Repository.Base;

namespace OnlinePaymentsApp.Repository.Interfaces.User
{
    public interface IUserRepository : IBaseRepository<Models.User, UserFilter, UserUpdate>
    {
    }
}
