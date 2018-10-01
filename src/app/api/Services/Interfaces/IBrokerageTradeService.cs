using Sans.CreditUnion.API.Features.BrokerageTrade.Models;
using Sans.CreditUnion.API.Features.BrokerageTrades.Models;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface IBrokerageTradeService
    {
        BrokerageTrade GetByGuidAndMemberId(string guid, long memberId);
        TradeStockResult TradeStock(TradeStockRequest request, long memberId);
    }
}