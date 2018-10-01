using Sans.CreditUnion.API.Features.Members.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class MembersApiWrapper
    {
        public const string Base = "/api/Members";
        public static Task<HttpResponseMessage> SendTravelAbroadPutRequestAsync(this HttpClient client, UpdateTravelAbroadRequest model)
        {
            return client.PutAsJsonAsync($"{Base}/UpdateTravelAbroad", model);
        }

        public static Task<HttpResponseMessage> SendMembersPostRequestAsync(this HttpClient client, CreateMemberRequest model)
        {
            return client.PostAsJsonAsync(Base, model);
        }
    }
}
