using Sans.CreditUnion.API.Features.CheckOrders.Models;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services
{
    public interface ICheckOrderService
    {
        CheckOrder GetCheckOrderByGuid(string guid);
        OrderChecksResult OrderChecks(OrderChecksRequest model);
    }
}