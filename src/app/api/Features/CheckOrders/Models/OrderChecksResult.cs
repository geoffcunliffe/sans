using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Features.CheckOrders.Models
{
    public class OrderChecksResult
    {
        public OrderChecksResult(OrderChecksResultType resultType, CheckOrder checkOrder)
        {
            ResultType = resultType;
            CheckOrder = checkOrder;
        }

        public OrderChecksResultType ResultType { get; set; }
        public CheckOrder CheckOrder { get; set; }
    }


    public enum OrderChecksResultType
    {
        AccountNotFound,
        Successful
    }
}
