using OnlinePaymentsApp.Repository.Interfaces.Account;
using OnlinePaymentsApp.Services.DTOs.Account;
using OnlinePaymentsApp.Services.DTOs.Account.Response;
using OnlinePaymentsApp.Services.Interfaces.Account;

namespace OnlinePaymentsApp.Services.Implementations.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }

        public async Task<GetAllAccountsResponse> GetAllAsync()
        {
            var accounts = await accountRepository.RetrieveCollectionAsync(new AccountFilter()).ToListAsync();

            var allAccountsResponse = new GetAllAccountsResponse
            {
                Accounts = accounts.Select(MapToAccountInfo).ToList(),
                TotalCount = accounts.Count
            };

            return allAccountsResponse;
        }

        public async Task<GetAccountResponse> GetByIdAsync(int accountId)
        {
            var account = await accountRepository.RetrieveAsync(accountId);

            if (account == null)
            {
                throw new Exception($"Account with ID {accountId} not found.");
            }

            return new GetAccountResponse
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance
            };
        }

        private AccountInfo MapToAccountInfo(Models.Account account)
        {
            return new AccountInfo
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance
            };
        }
    }
}
