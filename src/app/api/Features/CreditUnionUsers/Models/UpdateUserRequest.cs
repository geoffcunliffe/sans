using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.CreditUnionUsers.Models
{
    public class UpdateUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
