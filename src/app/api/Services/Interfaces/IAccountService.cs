using System.Collections.Generic;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface IAccountService
    {
        List<Account> GetAllAccountsByUserId(string userId);
    }
}