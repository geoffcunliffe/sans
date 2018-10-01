using Sans.CreditUnion.API.Features.Authentication.Models;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Services.Interfaces
{
    public interface ISignInService
    {
        Task<CheckPasswordResult> CheckPasswordAsync(string email, string password);
    }
}