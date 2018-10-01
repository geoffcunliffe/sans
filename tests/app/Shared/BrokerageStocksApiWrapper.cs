using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class BrokerageStocksApiWrapper
    {
        public const string Base = "/api/BrokerageStocks";
        public static Task<HttpResponseMessage> SendBrokerageStocksGetRequest(this HttpClient client)
        {
            return client.GetAsync(Base);
        }

        public static Task<HttpResponseMessage> SendBrokerageStocksGetRequest(this HttpClient client, long id)
        {
            return client.GetAsync($"{Base}/{id}");
        }
    }
}
