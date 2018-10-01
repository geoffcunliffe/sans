using Microsoft.AspNetCore.Identity;
using System;

namespace Sans.CreditUnion.API.Infrastructure.Constants
{
    public class OptionConstants
    {
        public static IdentityOptions IdentityOptions => new IdentityOptions
        {
            Password = new PasswordOptions
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireNonAlphanumeric = true,
                RequireUppercase = true,
                RequireLowercase = true,
            },
            Lockout = new LockoutOptions
            {
                DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15),
                MaxFailedAccessAttempts  = 5
            },
            User = new UserOptions
            {
                RequireUniqueEmail = true
            }
        };
    }
}
