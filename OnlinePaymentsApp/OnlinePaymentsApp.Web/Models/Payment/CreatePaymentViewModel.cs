using OnlinePaymentsApp.Web.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace OnlinePaymentsApp.Web.Models.Payment
{
    public class CreatePaymentViewModel
    {
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Банковата сметка е необходима.")]
        [Display(Name = "От сметка")]
        public int FromAccountId { get; set; }

        [Required(ErrorMessage = "Моля, посочете банкова сметка.")]
        [StringLength(22, ErrorMessage = "Account number must be exactly 22 characters long.")]
        [RegularExpression("^[A-Za-z0-9]{22}$", ErrorMessage = "Моля, въведете банкова сметка, съдържаща само латински букви и цифри.")]
        [Display(Name = "Към сметка")]
        public string ToAccountNumber { get; set; }

        [Required(ErrorMessage = "Сумата е задължително поле.")]
        [Range(0, double.MaxValue, ErrorMessage = "Моля, въведете валидна сума.")]
        [Display(Name = "Сума")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Моля, посочете основание.")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Основанието трябва да е дълго от 1 до 32 символа.")]
        [Display(Name = "Основание")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Creation time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [Required(ErrorMessage = "Created by user is required.")]
        public int CreatedByUserId { get; set; }

        public List<AccountSelectItem> UserAccounts { get; set; } = new();
    }

    public class AccountSelectItem
    {
        public int AccountId { get; set; }
        public string DisplayText { get; set; } = null!;
    }
}
