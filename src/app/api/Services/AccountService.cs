using System.Collections.Generic;
using System.Linq;
using Sans.CreditUnion.API.Services.Interfaces;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly SansCreditUnionDbContext _dbContext;
        public AccountService(SansCreditUnionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Account> GetAllAccountsByUserId(string userId)
        {
            return _dbContext.Accounts
                .Where(a => a.Member.User.Id == userId)
                .ToList();
        }
    }
}