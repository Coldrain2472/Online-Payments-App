using System.ComponentModel.DataAnnotations;

namespace OnlinePaymentsApp.Web.Models.Payment
{
    public class PaymentListViewModel
    {
        public List<PaymentViewModel> Payments { get; set; }

        public int TotalCount { get; set; }
    }

    public class PaymentViewModel
    {
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Моля, изберете банкова сметка.")]
        public int FromAccountId { get; set; }

        [Required(ErrorMessage = "Моля, въведете банкова сметка.")]
        [StringLength(22, ErrorMessage = "Банковата сметка трябва да е дълга 22 символа.")]
        public string ToAccountNumber { get; set; }

        [Required(ErrorMessage = "Сумата е задължителна.")]
        [Range(0, double.MaxValue, ErrorMessage = "Сумата трябва да е положително число.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Основанието е задължително.")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Основанието трябва да е между 1 и 32 символа.")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Creation time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Created by user is required.")]
        public int CreatedByUserId { get; set; }

        public string? FromAccountNumber { get; set; }
    }
}
