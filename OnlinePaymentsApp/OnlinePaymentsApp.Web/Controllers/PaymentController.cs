using Microsoft.AspNetCore.Mvc;
using OnlinePaymentsApp.Services.DTOs.Payment.Request;
using OnlinePaymentsApp.Services.Interfaces.Account;
using OnlinePaymentsApp.Services.Interfaces.Payment;
using OnlinePaymentsApp.Services.Interfaces.User;
using OnlinePaymentsApp.Services.Interfaces.UserAccount;
using OnlinePaymentsApp.Web.Attributes;
using OnlinePaymentsApp.Web.Helpers;
using OnlinePaymentsApp.Web.Models.Payment;

namespace OnlinePaymentsApp.Web.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService paymentService;
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IUserAccountService userAccountService;

        public PaymentController(IPaymentService _paymentService, IAccountService _accountService,
            IUserService _userService, IUserAccountService _userAccountService)
        {
            paymentService = _paymentService;
            accountService = _accountService;
            userService = _userService;
            userAccountService = _userAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = IdHelper.GetUserId(HttpContext);
            var paymentsChronological = await paymentService.GetAllChronologicallyAsync(userId);
            var accountIds = paymentsChronological.Payments.Select(p => p.FromAccountId).Distinct();
            var accounts = await accountService.GetAllAsync(); 

            var viewModel = new PaymentListViewModel
            {
                Payments = paymentsChronological.Payments.Select(p => new PaymentViewModel
                {
                    PaymentId = p.PaymentId,
                    FromAccountId = p.FromAccountId,
                    ToAccountNumber = p.ToAccountNumber,
                    Amount = p.Amount,
                    Reason = p.Reason,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt,
                    CreatedByUserId = p.CreatedByUserId,
                    FromAccountNumber = accountIds.Contains(p.FromAccountId) 
                        ? accounts.Accounts.FirstOrDefault(a => a.AccountId == p.FromAccountId)?.AccountNumber 
                        : null
                }).ToList(),
                TotalCount = paymentsChronological.TotalCount
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Filter()
        {
            var userId = IdHelper.GetUserId(HttpContext);
            var paymentsByStatus = await paymentService.GetAllByStatusAsync(userId);
            var accountIds = paymentsByStatus.Payments.Select(p => p.FromAccountId).Distinct();
            var accounts = await accountService.GetAllAsync();

            var viewModel = new PaymentListViewModel
            {
                Payments = paymentsByStatus.Payments.Select(p => new PaymentViewModel
                {
                    PaymentId = p.PaymentId,
                    FromAccountId = p.FromAccountId,
                    ToAccountNumber = p.ToAccountNumber,
                    Amount = p.Amount,
                    Reason = p.Reason,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt,
                    CreatedByUserId = p.CreatedByUserId,
                    FromAccountNumber = accountIds.Contains(p.FromAccountId)
                        ? accounts.Accounts.FirstOrDefault(a => a.AccountId == p.FromAccountId)?.AccountNumber
                        : null
                }).ToList(),
                TotalCount = paymentsByStatus.TotalCount
            };

            return View(nameof(Index), viewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = IdHelper.GetUserId(HttpContext);

            if (userId == null)
            {
                return RedirectToAction("Login", "Authentication");
            }

            var userAccounts = await userAccountService.GetAllUserAccountsAsync(new Services.DTOs.UserAccount.Request.GetAllUserAccountsRequest
            {
                UserId = userId
            });

            var userAccountsResult = userAccounts.Accounts.Select(a => new AccountSelectItem
            {
                AccountId = a.AccountId,
                DisplayText = $"{a.AccountNumber} - {a.Balance:C2}"
            })
                .ToList();

            var viewModel = new CreatePaymentViewModel
            {
                UserAccounts = userAccountsResult
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentViewModel model)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var user = await userService.GetByIdAsync(currentUserId);
            var request = new CreatePaymentRequest
            {
                Amount = model.Amount,
                CreatedAt = DateTime.Now,
                FromAccountId = model.FromAccountId,
                ToAccountNumber = model.ToAccountNumber,
                CreatedByUserId = currentUserId,
                PaymentId = model.PaymentId,
                Reason = model.Reason,
                Status = "ИЗЧАКВА"
            };

            var response = await paymentService.CreateAsync(request);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Send(int paymentId)
        {
            var response = await paymentService.SendAsync(new SendPaymentRequest
            {
                PaymentId = paymentId
            });

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = $"Плащането е изпратено успешно. Статус: {response.Status}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int paymentId)
        {
            if (paymentId <= 0)
            {
                TempData["ErrorMessage"] = "Невалиден идентификатор на плащането.";
                return RedirectToAction(nameof(Index));
            }

            var response = await paymentService.CancelAsync(new CancelPaymentRequest
            {
                PaymentId = paymentId
            });

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }
            else
            {
                TempData["SuccessMessage"] = $"Плащането е отказано успешно. Нов статус: {response.Status}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
