using System.ComponentModel.DataAnnotations;

namespace OnlinePaymentsApp.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Account is required.")]
        public int FromAccountId { get; set; }

        [Required(ErrorMessage = "Account number is required.")]
        [StringLength(22, ErrorMessage = "Account number must be exactly 22 characters long.")]
        public string ToAccountNumber { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Reason is required.")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Reason must be between 1 and 32 characters.")]
        public string Reason { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Creation time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Created by user is required.")]
        public int CreatedByUserId { get; set; }
    }
}
