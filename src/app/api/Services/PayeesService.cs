using Sans.CreditUnion.API.Features.Payees.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System.Linq;

namespace Sans.CreditUnion.API.Services
{
    public class PayeesService : IPayeesService
    {
        private readonly SansCreditUnionDbContext _dbContext;
        public PayeesService(SansCreditUnionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Payee AddPayee(AddPayeeRequest model)
        {
            var payee = new Payee
            {
                City = model.City,
                Name = model.Name,
                State = model.State,
                Street1 = model.Street1,
                Street2 = model.Street2,
                Zip4 = model.Zip4,
                Zip5 = model.Zip5
            };

            _dbContext.Payees.Add(payee);
            _dbContext.SaveChanges();

            return payee;
        }

        public Payee GetPayeeByGuidAndMemberId(string guid, long memberId)
        {
            return _dbContext.Payees
                .SingleOrDefault(p => p.Guid == guid && p.MemberIdWhoAdded == memberId);
        }
    }
}
