using Sans.CreditUnion.API.Features.CheckOrders.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class CheckOrdersApiWrapper
    {
        public static Task<HttpResponseMessage> SendCheckOrdersPostRequestAsync(this HttpClient client, OrderChecksRequest model)
        {
            return client.PostAsJsonAsync("/api/CheckOrders", model);
        }
    }
}
