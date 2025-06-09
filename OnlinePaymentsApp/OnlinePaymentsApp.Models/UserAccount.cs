using System.ComponentModel.DataAnnotations;

namespace OnlinePaymentsApp.Models
{
    public class UserAccount
    {
        [Required(ErrorMessage = "User is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Account is required.")]
        public int AccountId { get; set; }
    }
}
