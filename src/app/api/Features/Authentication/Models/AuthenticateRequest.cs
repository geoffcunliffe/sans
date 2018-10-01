using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.Authentication.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
