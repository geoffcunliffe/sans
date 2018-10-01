using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sans.CreditUnion.API.Features.BrokerageTrade.Models;
using Sans.CreditUnion.API.Features.BrokerageTrades.Models;
using Sans.CreditUnion.API.Services.Interfaces;

namespace Sans.CreditUnion.API.Features.BrokerageTrade
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrokerageTradeController : Controller
    {
        private readonly IBrokerageTradeService _brokerageTradeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrokerageTradeController(IBrokerageTradeService brokerageTradeService, IHttpContextAccessor httpContextAccessor)
        {
            _brokerageTradeService = brokerageTradeService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{guid}")]
        public ActionResult<Database.Models.BrokerageTrade> Get(string guid)
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            var trade = _brokerageTradeService.GetByGuidAndMemberId(guid, memberId);

            if (trade == null)
                return NotFound();

            return trade;
        }

        [HttpPost]
        public ActionResult<Database.Models.BillPay> Post(TradeStockRequest request)
        {
            long memberId = _httpContextAccessor.HttpContext.User.GetMemberId();
            var result = _brokerageTradeService.TradeStock(request, memberId);

            if (result.ResultType == TradeStockType.InvalidTradeType)
                return NotFound($"Trade type {request.TradeType} not found.");

            if (result.ResultType == TradeStockType.AccountNotFoundForMember)
                return NotFound($"Account with guid {request.AccountGuid} not found.");

            if (result.ResultType == TradeStockType.BrokerageAccountNotFoundForMember)
                return NotFound($"Brokerage Account with guid {request.BrokerageAccountGuid} not found.");

            if (result.ResultType == TradeStockType.StockNotFound)
                return NotFound($"Stock with ID {request.StockId} not found.");

            return CreatedAtAction(nameof(Get), new { guid = result.Trade.Guid }, result.Trade);
        }
    }
}