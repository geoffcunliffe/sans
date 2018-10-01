using Sans.CreditUnion.Database.Models;
using System.Collections.Generic;

namespace Sans.CreditUnion.API.Features.Members.Models
{
    public class CreateMemberRequestResult
    {
        public Member Member { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool WasSuccessful => Errors.Count == 0;
    }
}
