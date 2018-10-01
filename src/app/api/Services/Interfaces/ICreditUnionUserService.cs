using Microsoft.AspNetCore.Identity;
using Sans.CreditUnion.Database.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Sans.CreditUnion.API.Features.CreditUnionUsers.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface ICreditUnionUserService
    {
        Task<IdentityResult> ChangePasswordAsync(CreditUnionUser currentUser, string currentPassword, string newPassword);
        Task<CreditUnionUser> FindByEmailAsync(string email);
        Task<CreditUnionUser> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<IdentityResult> UpdateContactInformation(CreditUnionUser currentUser, UpdateUserRequest request);
        Task<IdentityResult> UpdateAsync(CreditUnionUser currentUser);
    }
}