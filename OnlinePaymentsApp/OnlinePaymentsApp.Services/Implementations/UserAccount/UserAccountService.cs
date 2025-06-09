using OnlinePaymentsApp.Repository.Interfaces.Account;
using OnlinePaymentsApp.Repository.Interfaces.User;
using OnlinePaymentsApp.Repository.Interfaces.UserAccount;
using OnlinePaymentsApp.Services.DTOs.Account;
using OnlinePaymentsApp.Services.DTOs.UserAccount.Request;
using OnlinePaymentsApp.Services.DTOs.UserAccount.Response;
using OnlinePaymentsApp.Services.Interfaces.UserAccount;

namespace OnlinePaymentsApp.Services.Implementations.UserAccount
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly IUserRepository userRepository;
        private readonly IAccountRepository accountRepository;

        public UserAccountService(IUserAccountRepository _userAccountRepository, IUserRepository _userRepository, IAccountRepository _accountRepository)
        {
            userAccountRepository = _userAccountRepository;
            userRepository = _userRepository;
            accountRepository = _accountRepository;
        }

        public async Task<AddUserAccountResponse> AddUserAccountAsync(AddUserAccountRequest request)
        {
            var user = await userRepository.RetrieveAsync(request.UserId);
            if (user == null)
            {
                return new AddUserAccountResponse
                {
                    Success = false,
                    ErrorMessage = "User not found."
                };
            }

            var account = await accountRepository.RetrieveAsync(request.AccountId);
            if (account == null)
            {
                return new AddUserAccountResponse
                {
                    Success = false,
                    ErrorMessage = "Account not found."
                };
            }

            var userAccounts = await userAccountRepository.RetrieveCollectionAsync(new UserAccountFilter { UserId = request.UserId }).ToListAsync();
            if (userAccounts.Any(ua => ua.AccountId == request.AccountId))
            {
                return new AddUserAccountResponse
                {
                    Success = false,
                    ErrorMessage = "User already has this account."
                };
            }

            var userAccount = new Models.UserAccount
            {
                UserId = request.UserId,
                AccountId = request.AccountId
            };

            await userAccountRepository.CreateAsync(userAccount);
            return new AddUserAccountResponse
            {
                Success = true
            };
        }

        public async Task<GetAllUserAccountsResponse> GetAllUserAccountsAsync(GetAllUserAccountsRequest request)
        {
            var userAccounts = await userAccountRepository.RetrieveCollectionAsync(new UserAccountFilter { UserId = request.UserId }).ToListAsync();

            var response = new GetAllUserAccountsResponse
            {
                UserId = request.UserId,
                Accounts = new List<AccountInfo>(),
                TotalCount = userAccounts.Count
            };

            foreach (var userAccount in userAccounts)
            {
                var account = await accountRepository.RetrieveAsync(userAccount.AccountId);
                if (account != null)
                {
                    response.Accounts.Add(new AccountInfo
                    {
                        AccountId = account.AccountId,
                        AccountNumber = account.AccountNumber,
                        Balance = account.Balance
                    });
                }
            }

            return response;
        }

        public async Task<RemoveUserAccountResponse> RemoveUserAccountAsync(RemoveUserAccountRequest request)
        {
            var userAccounts = await userAccountRepository.RetrieveCollectionAsync(new UserAccountFilter { UserId = request.UserId, AccountId = request.AccountId }).ToListAsync();

            var toRemove = userAccounts.FirstOrDefault();
            if (toRemove == null)
            {
                return new RemoveUserAccountResponse
                {
                    Success = false,
                    ErrorMessage = "User account not found."
                };
            }

            await userAccountRepository.DeleteAsync(toRemove.UserId, toRemove.AccountId);

            return new RemoveUserAccountResponse
            {
                Success = true
            };
        }
    }
}
