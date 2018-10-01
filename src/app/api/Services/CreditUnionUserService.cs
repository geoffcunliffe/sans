using Microsoft.AspNetCore.Identity;
using Sans.CreditUnion.API.Features.CreditUnionUsers.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Services
{
    public class CreditUnionUserService : ICreditUnionUserService
    {
        private readonly UserManager<CreditUnionUser> _userManager;

        public CreditUnionUserService(UserManager<CreditUnionUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityResult> ChangePasswordAsync(CreditUnionUser currentUser, string currentPassword, string newPassword)
        {
            return _userManager.ChangePasswordAsync(currentUser, currentPassword, newPassword);
        }

        public Task<CreditUnionUser> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<CreditUnionUser> GetUserAsync(ClaimsPrincipal claimsPrincipal)
        {
            return _userManager.GetUserAsync(claimsPrincipal);
        }

        public async Task<IdentityResult> UpdateContactInformation(CreditUnionUser currentUser, UpdateUserRequest request)
        {
            if (currentUser.Email != request.Email)
            {
                // TODO: do we want to confirm the email first?
                var result = await _userManager.SetEmailAsync(currentUser, request.Email);

                if (!result.Succeeded)
                    return result;
            }

            if (currentUser.PhoneNumber != request.PhoneNumber)
            {
                // TODO: do we want to confirm their Phone # first?  Do we even want Phone # in this app?
                var result = await _userManager.SetPhoneNumberAsync(currentUser, request.PhoneNumber);

                if (!result.Succeeded)
                    return result;
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> UpdateAsync(CreditUnionUser currentUser)
        {
            return _userManager.UpdateAsync(currentUser);
        }
    }
}
