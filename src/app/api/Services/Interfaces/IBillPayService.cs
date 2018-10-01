using System.Collections.Generic;
using Sans.CreditUnion.API.Features.BillPay.Models;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface IBillPayService
    {
        AddBillPayResult AddBillPay(AddBillPayRequest model, long memberId);
        DeleteBillPayResult DeleteBillPay(string guid, long memberId);
        List<BillPay> GetAllByMemberId(long memberId);
        BillPay GetByGuidAndMemberId(string guid, long memberId);
    }
}