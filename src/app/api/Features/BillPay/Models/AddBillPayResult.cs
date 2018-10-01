using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Features.BillPay.Models
{
    public class AddBillPayResult
    {
        public AddBillPayResult(AddBillPayResultType resultType, Database.Models.BillPay billPay)
        {
            ResultType = resultType;
            BillPay = billPay;
        }

        public AddBillPayResultType ResultType { get; set; }
        public Database.Models.BillPay BillPay { get; set; }
    }

    public enum AddBillPayResultType
    {
        PayeeNotFound,
        Successful
    }
}
