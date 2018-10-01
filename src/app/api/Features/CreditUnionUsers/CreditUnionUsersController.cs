using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Features.CreditUnionUsers.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Features.CreditUnionUsers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditUnionUsersController : Controller
    {
        private readonly ICreditUnionUserService _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreditUnionUsersController(ICreditUnionUserService userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            CreditUnionUser currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            
            IdentityResult result = await _userManager.ChangePasswordAsync(currentUser, request.CurrentPassword, request.NewPassword);

            if (!result.Succeeded)
            {
                ModelState.AddIdentityErrorsToModelState(result);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest request)
        {
            CreditUnionUser currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            
            IdentityResult result = await _userManager.UpdateContactInformation(currentUser, request);

            if (!result.Succeeded)
            {
                ModelState.AddIdentityErrorsToModelState(result);
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}