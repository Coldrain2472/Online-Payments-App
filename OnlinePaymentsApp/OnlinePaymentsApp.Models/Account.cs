using System.ComponentModel.DataAnnotations;

namespace OnlinePaymentsApp.Models
{
    public class Account
    {
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Account number is required.")]
        [StringLength(22, ErrorMessage = "Account number must be exactly 22 characters long.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a non-negative value.")]
        public decimal Balance { get; set; }
    }
}
