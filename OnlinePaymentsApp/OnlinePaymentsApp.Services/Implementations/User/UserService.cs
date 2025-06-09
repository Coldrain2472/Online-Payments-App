using OnlinePaymentsApp.Repository.Interfaces.User;
using OnlinePaymentsApp.Services.DTOs.User;
using OnlinePaymentsApp.Services.DTOs.User.Response;
using OnlinePaymentsApp.Services.Interfaces.User;

namespace OnlinePaymentsApp.Services.Implementations.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task<GetAllUsersResponse> GetAllAsync()
        {
            var users = await userRepository.RetrieveCollectionAsync(new UserFilter()).ToListAsync();

            var allUsersResponse = new GetAllUsersResponse
            {
                Users = users.Select(MapToEmployeeInfo).ToList(),
                TotalCount = users.Count
            };

            return allUsersResponse;
        }

        public async Task<GetUserResponse> GetByIdAsync(int userId)
        {
            var employee = await userRepository.RetrieveAsync(userId);

            return new GetUserResponse
            {
                UserId = userId,
                Name = employee.Name,
                Username = employee.Username
            };
        }

        private UserInfo MapToEmployeeInfo(Models.User user)
        {
            return new UserInfo
            {
                UserId = user.UserId,
                Name = user.Name,
                Username = user.Username
            };
        }
    }
}
