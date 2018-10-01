using System.Threading.Tasks;
using Sans.CreditUnion.API.Features.Members.Models;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface IMembersService
    {
        Task<CreateMemberRequestResult> CreateAsync(CreateMemberRequest request);
        Member GetByMemberId(long memberId);
        UpdateTravelAbroadResult UpdateTravelAbroad(UpdateTravelAbroadRequest model, long memberId);
    }
}