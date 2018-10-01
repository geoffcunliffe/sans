using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class TransactionsApiWrapper
    {
        public static async Task<HttpResponseMessage> SendTransactionsGetRequestAsync(this HttpClient client)
        {
            var result = await client.GetAsync("/api/Transactions");

            return result;
        }
    }
}
