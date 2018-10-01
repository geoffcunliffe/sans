using Microsoft.AspNetCore.Identity;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Features.Authentication.Models
{
    public class CheckPasswordResult
    {
        public CheckPasswordResult(SignInResult result, CreditUnionUser user)
        {
            Result = result;
            User = user;
        }

        public SignInResult Result { get; }
        public CreditUnionUser User { get; }
    }
}