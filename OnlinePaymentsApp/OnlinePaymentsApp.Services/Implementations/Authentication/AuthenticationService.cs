using OnlinePaymentsApp.Repository.Interfaces.User;
using OnlinePaymentsApp.Services.DTOs.Authentication.Request;
using OnlinePaymentsApp.Services.DTOs.Authentication.Response;
using OnlinePaymentsApp.Services.Helpers;
using OnlinePaymentsApp.Services.Interfaces.Authentication;
using System.Data.SqlTypes;

namespace OnlinePaymentsApp.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;

        public AuthenticationService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Username and password are required."
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new UserFilter { Username = new SqlString(request.Username) };

            var users = await userRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var user = users.SingleOrDefault();

            if (user == null || user.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password."
                };
            }

            return new LoginResponse
            {
                Success = true,
                UserId = user.UserId,
                Name = user.Name,
                Username = user.Username
            };
        }
    }
}
