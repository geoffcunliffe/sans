namespace Sans.CreditUnion.API.Features.BrokerageTrades.Models
{
    public class TradeStockResult
    {
        public TradeStockResult(TradeStockType resultType, Database.Models.BrokerageTrade trade)
        {
            ResultType = resultType;
            Trade = trade;
        }

        public Database.Models.BrokerageTrade Trade { get; }
        public TradeStockType ResultType { get; }
    }

    public enum TradeStockType
    {
        AccountNotFoundForMember,
        BrokerageAccountNotFoundForMember,
        InvalidTradeType,
        StockNotFound,
        Successful
    }
}
