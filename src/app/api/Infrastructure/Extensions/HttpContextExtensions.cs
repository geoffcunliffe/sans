using System;
using System.Security.Claims;
using Sans.CreditUnion.API.Infrastructure.Constants;

namespace Sans.CreditUnion.API
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static long GetMemberId(this ClaimsPrincipal principal)
        {
            return Convert.ToInt64(principal.FindFirst(c => c.Type == JwtClaimTypes.MemberId).Value);
        }
    }
}