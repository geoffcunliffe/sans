using Microsoft.AspNetCore.Identity;
using Sans.CreditUnion.API.Features.Members.Models;
using Sans.CreditUnion.API.Infrastructure.Models;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Services
{
    public class MembersService : IMembersService
    {
        private readonly UserManager<CreditUnionUser> _userManager;
        private readonly SansCreditUnionDbContext _dbContext;

        public MembersService(UserManager<CreditUnionUser> userManager, SansCreditUnionDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<CreateMemberRequestResult> CreateAsync(CreateMemberRequest request)
        {
            var member = new Member
            {
                City = request.City,
                FirstName = request.FirstName,
                LastName = request.LastName,
                State = request.State,
                Street = request.Street,
                ZipCode = request.ZipCode
            };

            var user = new CreditUnionUser
            {
                Email = request.Email,
                LockoutEnabled = true,
                UserName = request.Email,
                Member = member
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new CreateMemberRequestResult
                {
                    Errors = result.Errors.Select(s => s.Description).ToList()
                };
            }

            return new CreateMemberRequestResult
            {
                Member = member
            };
        }

        public Member GetByMemberId(long memberId)
        {
            return _dbContext.Members
                .Where(m => m.Id == memberId)
                .Single();
        }

        public UpdateTravelAbroadResult UpdateTravelAbroad(UpdateTravelAbroadRequest model, long memberId)
        {
            var result = new UpdateTravelAbroadResult();
            if (model.EndDate.Value < model.StartDate.Value)
            {
                result.ValidationErrors.Add(new ValidationError(nameof(model.EndDate), "End Date must be after Start Date."));
                return result;
            }

            Member member = GetByMemberId(memberId);

            member.TravelingAbroadStart = model.StartDate;
            member.TravelingAbroadEnd = model.EndDate;

            _dbContext.SaveChanges();

            return result;
        }
    }
}
