using Microsoft.AspNetCore.Mvc;
using OnlinePaymentsApp.Services.Interfaces.UserAccount;
using OnlinePaymentsApp.Web.Attributes;
using OnlinePaymentsApp.Web.Helpers;
using OnlinePaymentsApp.Web.Models.Account;

namespace OnlinePaymentsApp.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserAccountService userAccountService;

        public AccountController(IUserAccountService _userAccountService)
        {
            userAccountService = _userAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = IdHelper.GetUserId(HttpContext);

            var userAccounts = await userAccountService.GetAllUserAccountsAsync(new Services.DTOs.UserAccount.Request.GetAllUserAccountsRequest
            {
                UserId = userId
            });

            var viewModel = new AccountListViewModel
            {
                Accounts = userAccounts.Accounts.Select(a => new AccountViewModel
                {
                    AccountId = a.AccountId,
                    AccountNumber = a.AccountNumber,
                    Balance = a.Balance
                })
                .ToList(),
                TotalCount = userAccounts.TotalCount
            };

            return View(viewModel);
        }
    }
}
