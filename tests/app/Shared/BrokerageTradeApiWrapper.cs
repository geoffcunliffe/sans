using Sans.CreditUnion.API.Features.BrokerageTrade.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class BrokerageTradeApiWrapper
    {
        public const string Base = "/api/BrokerageTrade";
        public static async Task<HttpResponseMessage> SendBrokerageTradePostRequest(this HttpClient client, TradeStockRequest request)
        {
            var result = await client.PostAsJsonAsync(Base, request);
            return result;
        }
    }
}
