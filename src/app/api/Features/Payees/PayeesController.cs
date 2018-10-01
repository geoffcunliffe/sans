using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Features.Payees.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Features.Payees
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayeesController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPayeesService _payeesService;

        public PayeesController(IHttpContextAccessor httpContextAccessor, IPayeesService payeesService)
        {
            _httpContextAccessor = httpContextAccessor;
            _payeesService = payeesService;
        }

        [HttpGet("{guid}")]
        public ActionResult<Payee> Get(string guid)
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            Payee payee = _payeesService.GetPayeeByGuidAndMemberId(guid, memberId);

            if (payee == null)
                return NotFound();

            return payee;
        }

        [HttpPost]
        public ActionResult<Payee> Post(AddPayeeRequest model)
        {
            Payee payee = _payeesService.AddPayee(model);

            return CreatedAtAction(nameof(Get), new { guid = payee.Guid }, payee);
        }
    }
}
