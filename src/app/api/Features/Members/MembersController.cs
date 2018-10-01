using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Features.Members.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Features.Members
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : Controller
    {
        private readonly IMembersService _membersService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MembersController(IMembersService membersService, IHttpContextAccessor httpContextAccessor)
        {
            _membersService = membersService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult<Member> Get()
        {
            // Because this is behind [Authorize] attribute (bc it's applied globally), they must be logged in to get here.
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            return _membersService.GetByMemberId(memberId);
        }

        [HttpPut("UpdateTravelAbroad")]
        public IActionResult UpdateTravelAbroad(UpdateTravelAbroadRequest model)
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            var result = _membersService.UpdateTravelAbroad(model, memberId);

            if (!result.WasSuccessful)
            {
                ModelState.AddValidationErrorsToModelState(result.ValidationErrors);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Member>> Post([FromBody]CreateMemberRequest request)
        {
            CreateMemberRequestResult result = await _membersService.CreateAsync(request);

            if (!result.WasSuccessful)
            {
                ModelState.AddErrorListToModelState(result.Errors);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Get), null, result.Member);
        }
    }
}
