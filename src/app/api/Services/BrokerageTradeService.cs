using Sans.CreditUnion.API.Features.BrokerageTrade.Models;
using Sans.CreditUnion.API.Features.BrokerageTrades.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Constants;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System.Linq;

namespace Sans.CreditUnion.API.Services
{
    public class BrokerageTradeService : IBrokerageTradeService
    {
        private readonly SansCreditUnionDbContext _dbContext;
        public BrokerageTradeService(SansCreditUnionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BrokerageTrade GetByGuidAndMemberId(string guid, long memberId)
        {
            return _dbContext.BrokerageTrades
                .Where(b => b.Guid == guid
                            && b.BrokerageAccount.MemberId == memberId)
                .SingleOrDefault();
        }

        public TradeStockResult TradeStock(TradeStockRequest request, long memberId)
        {
            if (request.TradeType != BrokerageTradeTypes.Buy && request.TradeType != BrokerageTradeTypes.Sell)
                return new TradeStockResult(TradeStockType.InvalidTradeType, null);

            long? brokerageAccountId = _dbContext.BrokerageAccounts
                .Where(a => a.Guid == request.BrokerageAccountGuid
                            && a.MemberId == memberId)
                .Select(a => a.Id)
                .SingleOrDefault();

            if (brokerageAccountId == null)
                return new TradeStockResult(TradeStockType.BrokerageAccountNotFoundForMember, null);

            // This is the account they're drawing from to Buy a stock or depositing into if they're Selling a stock
            long? accountId = _dbContext.Accounts
                .Where(a => a.Guid == request.AccountGuid
                            && a.MemberId == memberId)
                .Select(a => a.Id)
                .SingleOrDefault();

            if (accountId == null)
                return new TradeStockResult(TradeStockType.AccountNotFoundForMember, null);

            var stock = _dbContext.BrokerageStocks
                .Where(s => s.Id == request.StockId)
                .SingleOrDefault();

            if (stock == null)
                return new TradeStockResult(TradeStockType.StockNotFound, null);

            var mostRecentPrice = _dbContext.BrokerageStockPriceHistory
                .Where(b => b.StockId == stock.Id)
                .OrderByDescending(b => b.Timestamp)
                .FirstOrDefault();

            var trade = new BrokerageTrade
            {
                BrokerageAccountId = brokerageAccountId.Value,
                StockId = stock.Id,
                Price = mostRecentPrice.Price,
                Quantity = request.Quantity,
                BrokerageTradeType = request.TradeType
            };

            _dbContext.BrokerageTrades.Add(trade);
            _dbContext.SaveChanges();

            return new TradeStockResult(TradeStockType.Successful, trade);
        }
    }
}
