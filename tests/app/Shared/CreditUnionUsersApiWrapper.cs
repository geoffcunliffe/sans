using Sans.CreditUnion.API.Features.CreditUnionUsers.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class CreditUnionUsersApiWrapper
    {
        public const string Base = "/api/CreditUnionUsers";
        public static Task<HttpResponseMessage> SendCreditUnionUsersPutRequest(this HttpClient client, UpdateUserRequest model)
        {
            return client.PutAsJsonAsync(Base, model);
        }
    }
}
