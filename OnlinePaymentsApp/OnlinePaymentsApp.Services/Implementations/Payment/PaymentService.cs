using OnlinePaymentsApp.Repository.Interfaces.Account;
using OnlinePaymentsApp.Repository.Interfaces.Payment;
using OnlinePaymentsApp.Repository.Interfaces.User;
using OnlinePaymentsApp.Repository.Interfaces.UserAccount;
using OnlinePaymentsApp.Services.DTOs.Payment;
using OnlinePaymentsApp.Services.DTOs.Payment.Request;
using OnlinePaymentsApp.Services.DTOs.Payment.Response;
using OnlinePaymentsApp.Services.Interfaces.Payment;

namespace OnlinePaymentsApp.Services.Implementations.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IUserRepository userRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IUserAccountRepository userAccountRepository;

        public PaymentService(IPaymentRepository _paymentRepository, IUserRepository _userRepository,
            IAccountRepository _accountRepository, IUserAccountRepository _userAccountRepository)
        {
            paymentRepository = _paymentRepository;
            userRepository = _userRepository;
            accountRepository = _accountRepository;
            userAccountRepository = _userAccountRepository;
        }

        public async Task<CancelPaymentResponse> CancelAsync(CancelPaymentRequest request)
        {
            var payment = await paymentRepository.RetrieveAsync(request.PaymentId);
            if (payment == null)
            {
                return new CancelPaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Payment not found."
                };
            }

            if (payment.Status != "ИЗЧАКВА")
            {
                return new CancelPaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Payment cannot be cancelled."
                };
            }

            var updateSuccess = await paymentRepository.UpdateAsync(payment.PaymentId, new PaymentUpdate
            {
                Status = "ОТКАЗАН"
            });

            if (!updateSuccess)
            {
                return new CancelPaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Failed to update payment status."
                };
            }


            return new CancelPaymentResponse
            {
                Success = true,
                Status = "ОТКАЗАН"
            };
        }

        public async Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest request)
        {
            var user = await userRepository.RetrieveAsync(request.CreatedByUserId);

            if (user == null)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "User not found."
                };
            }

            var account = await accountRepository.RetrieveAsync(request.FromAccountId);
            if (account == null)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Account not found."
                };
            }

            if (account.Balance < request.Amount)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Insufficient funds."
                };
            }
            if (request.ToAccountNumber.Length != 22)
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Account number must be exactly 22 characters long."
                };
            }

            if (string.IsNullOrWhiteSpace(request.ToAccountNumber)
                || request.ToAccountNumber.Length != 22 ||
                !request.ToAccountNumber.All(c => char.IsLetterOrDigit(c) && c <= 127))
            {
                return new CreatePaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Account number must be exactly 22 characters long and contain only Latin letters and digits."
                };
            }

            var newPayment = new Models.Payment
            {
                Amount = request.Amount,
                ToAccountNumber = request.ToAccountNumber,
                FromAccountId = request.FromAccountId,
                CreatedAt = DateTime.Now,
                CreatedByUserId = request.CreatedByUserId,
                Reason = request.Reason,
                Status = "ИЗЧАКВА"
            };

            var newPaymentId = await paymentRepository.CreateAsync(newPayment);

            return new CreatePaymentResponse
            {
                Success = true,
                PaymentId = newPaymentId,
            };
        }

        public async Task<GetAllPaymentsResponse> GetAllByStatusAsync(int userId)
        {
            var filter = new PaymentFilter
            {
                CreatedByUserId = userId
            };

            var payments = await paymentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var allPaymentsResponse = new GetAllPaymentsResponse
            {
                Payments = payments.Select(MapToPaymentInfo).OrderBy(p => p.Status).ToList(), // "ИЗЧАКВА" must be on top
                TotalCount = payments.Count
            };

            return allPaymentsResponse;
        }

        public async Task<GetAllPaymentsResponse> GetAllChronologicallyAsync(int userId)
        {
            var filter = new PaymentFilter
            {
                CreatedByUserId = userId
            };

            var payments = await paymentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var allPaymentsResponse = new GetAllPaymentsResponse
            {
                Payments = payments.Select(MapToPaymentInfo).OrderByDescending(p => p.CreatedAt).ToList(),
                TotalCount = payments.Count
            };

            return allPaymentsResponse;
        }

        public async Task<GetPaymentResponse> GetByIdAsync(int paymentId)
        {
            var payment = await paymentRepository.RetrieveAsync(paymentId);

            return new GetPaymentResponse
            {
                Amount = payment.Amount,
                ToAccountNumber = payment.ToAccountNumber,
                FromAccountId = payment.FromAccountId,
                CreatedAt = payment.CreatedAt,
                CreatedByUserId = payment.CreatedByUserId,
                PaymentId = payment.PaymentId,
                Reason = payment.Reason,
                Status = payment.Status
            };
        }

        public async Task<SendPaymentResponse> SendAsync(SendPaymentRequest request)
        {
            var payment = await paymentRepository.RetrieveAsync(request.PaymentId);
            if (payment == null)
            {
                return new SendPaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Payment not found."
                };
            }

            if (payment.Status != "ИЗЧАКВА")
            {
                return new SendPaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Payment cannot be sent."
                };
            }

            var account = await accountRepository.RetrieveAsync(payment.FromAccountId);
            if (account == null)
            {
                return new SendPaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Account not found."
                };
            }

            if (account.Balance < payment.Amount)
            {
                return new SendPaymentResponse
                {
                    Success = false,
                    ErrorMessage = "Insufficient funds."
                };
            }

            // deducting the amount from the balance and updating the payment status
            var updateBalance = account.Balance -= payment.Amount;
            await accountRepository.UpdateAsync(account.AccountId, new AccountUpdate { Balance = updateBalance });

            var updateStatus = payment.Status = "ОБРАБОТЕН";
            await paymentRepository.UpdateAsync(request.PaymentId, new PaymentUpdate { Status = updateStatus });

            var toAccount = await accountRepository.GetByAccountNumberAsync(payment.ToAccountNumber);
            if (toAccount != null)
            {
                toAccount.Balance += payment.Amount;
                await accountRepository.UpdateAsync(toAccount.AccountId, new AccountUpdate
                {
                    Balance = toAccount.Balance
                });
            }

            return new SendPaymentResponse
            {
                Success = true,
                Status = payment.Status
            };
        }

        private PaymentInfo MapToPaymentInfo(Models.Payment payment)
        {
            return new PaymentInfo
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt,
                FromAccountId = payment.FromAccountId,
                ToAccountNumber = payment.ToAccountNumber,
                CreatedByUserId = payment.CreatedByUserId,
                Reason = payment.Reason
            };
        }
    }
}
