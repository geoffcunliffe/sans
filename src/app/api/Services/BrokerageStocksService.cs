using Microsoft.EntityFrameworkCore;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sans.CreditUnion.API.Services
{
    public class BrokerageStocksService : IBrokerageStocksService
    {
        private readonly SansCreditUnionDbContext _dbContext;

        public BrokerageStocksService(SansCreditUnionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BrokerageStock> GetAll()
        {
            return _dbContext.BrokerageStocks.ToList();
        }

        public BrokerageStock GetById(long id)
        {
            return _dbContext.BrokerageStocks
                .Include(b => b.PriceHistories)
                .Where(b => b.Id == id)
                .SingleOrDefault();
        }
    }
}
