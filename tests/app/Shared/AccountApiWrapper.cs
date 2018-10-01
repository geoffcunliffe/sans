using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class AccountApiWrapper
    {
        public static Task<HttpResponseMessage> SendAccountGetRequestAsync(this HttpClient client)
        {
            return client.GetAsync("/api/Account");
        }
    }
}
