using System.Collections.Generic;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface IBrokerageStocksService
    {
        List<BrokerageStock> GetAll();
        BrokerageStock GetById(long id);
    }
}