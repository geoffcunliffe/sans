using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System.Collections.Generic;
using System.Linq;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.API.Features.BillPay.Models;

namespace Sans.CreditUnion.API.Services
{
    public class BillPayService : IBillPayService
    {
        private readonly SansCreditUnionDbContext _dbContext;
        public BillPayService(SansCreditUnionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BillPay> GetAllByMemberId(long memberId)
        {
            return _dbContext.BillPays
                .Where(a => a.Member.Id == memberId)
                .ToList();
        }

        public BillPay GetByGuidAndMemberId(string guid, long memberId)
        {
            return _dbContext.BillPays
                .SingleOrDefault(b => b.Guid == guid && b.MemberId == memberId);
        }

        public AddBillPayResult AddBillPay(AddBillPayRequest model, long memberId)
        {
            var payeeId = _dbContext.Payees
                .Where(p => p.Guid == model.PayeeGuid)
                .Select(p => p.Id)
                .SingleOrDefault();

            if (payeeId == 0)
                return new AddBillPayResult(AddBillPayResultType.PayeeNotFound, null);

            var billPay = new BillPay
            {
                Amount = model.Amount.GetValueOrDefault(),
                Frequency = model.Frequency,
                IsRecurring = model.IsRecurring,
                MemberId = memberId,
                NextPaymentDate = model.FirstPaymentDate,
                PayeeId = payeeId,
            };

            _dbContext.Add(billPay);
            _dbContext.SaveChanges();

            return new AddBillPayResult(AddBillPayResultType.Successful, billPay);
        }

        public DeleteBillPayResult DeleteBillPay(string guid, long memberId)
        {
           var billPay = _dbContext.BillPays
                .SingleOrDefault(b => b.Guid == guid && b.MemberId == memberId);

            if (billPay == null)
                return DeleteBillPayResult.NotFound;

            _dbContext.Remove(billPay);
            _dbContext.SaveChanges();

            return DeleteBillPayResult.Successful;
        }
    }
}
