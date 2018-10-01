using Microsoft.AspNetCore.Identity;
using Sans.CreditUnion.API.Features.Authentication.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Services
{
    public class SignInService : ISignInService
    {
        private readonly ICreditUnionUserService _creditUnionUserService;
        private readonly SignInManager<CreditUnionUser> _signInManager;

        public SignInService(ICreditUnionUserService creditUnionUserService, SignInManager<CreditUnionUser> signInManager)
        {
            _creditUnionUserService = creditUnionUserService;
            _signInManager = signInManager;
        }

        public async Task<CheckPasswordResult> CheckPasswordAsync(string email, string password)
        {
            var user = await _creditUnionUserService.FindByEmailAsync(email);

            if (user == null)
                return new CheckPasswordResult(SignInResult.Failed, null);

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
            return new CheckPasswordResult(result, user);
        }
    }
}
