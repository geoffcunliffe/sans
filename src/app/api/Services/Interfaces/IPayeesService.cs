using Sans.CreditUnion.API.Features.Payees.Models;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface IPayeesService
    {
        Payee AddPayee(AddPayeeRequest model);
        Payee GetPayeeByGuidAndMemberId(string guid, long memberId);
    }
}