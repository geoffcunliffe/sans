using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.CreditUnionUsers.Models
{
    public class ChangePasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "New Password and Confirm Password must match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
