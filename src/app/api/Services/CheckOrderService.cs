using Sans.CreditUnion.API.Features.CheckOrders.Models;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System.Linq;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public class CheckOrderService : ICheckOrderService
    {
        private readonly SansCreditUnionDbContext _dbContext;

        public CheckOrderService(SansCreditUnionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CheckOrder GetCheckOrderByGuid(string guid)
        {
            return _dbContext.CheckOrders
                .Where(co => co.Guid == guid)
                .SingleOrDefault();
        } 

        public OrderChecksResult OrderChecks(OrderChecksRequest model)
        {
            var accountId = _dbContext.Accounts
                .Where(a => a.Guid == model.AccountGuid)
                .Select(a => a.Id) 
                .SingleOrDefault();

            if (accountId == 0)
                return new OrderChecksResult(OrderChecksResultType.AccountNotFound, null);

            var checkOrder = new CheckOrder
            {
                AccountId = accountId,
            };

            _dbContext.CheckOrders.Add(checkOrder);
            _dbContext.SaveChanges();

            return new OrderChecksResult(OrderChecksResultType.Successful, checkOrder);
        }
    }
}
