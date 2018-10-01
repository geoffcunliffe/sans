using Microsoft.AspNetCore.Identity;

namespace Sans.CreditUnion.Database.Models
{
    public class CreditUnionUser : IdentityUser
    {
        public long MemberId { get; set; }
        public Member Member { get; set; }
    }
}
