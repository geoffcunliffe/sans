using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Features.CheckOrders.Models;
using Sans.CreditUnion.API.Services;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Features.CheckOrders
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckOrdersController : Controller
    {
        private readonly ICheckOrderService _checkOrderService;

        public CheckOrdersController(ICheckOrderService checkOrderService)
        {
            _checkOrderService = checkOrderService;
        }

        [HttpGet("{guid}")]
        public ActionResult<CheckOrder> Get(string guid)
        {
            var checkOrder = _checkOrderService.GetCheckOrderByGuid(guid);

            if (checkOrder == null)
                return NotFound();

            return checkOrder;
        }

        [HttpPost]
        public ActionResult<CheckOrder> Post(OrderChecksRequest model)
        {
            var result = _checkOrderService.OrderChecks(model);

            if (result.ResultType == OrderChecksResultType.AccountNotFound)
                return NotFound($"Account with guid {model.AccountGuid} not found.");

            return CreatedAtAction(nameof(Get), new { guid = result.CheckOrder.Guid }, result.CheckOrder);
        }
    }
}
