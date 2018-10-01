using Sans.CreditUnion.API.Features.Payees.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class PayeesApiWrapper
    {
        public static async Task<HttpResponseMessage> SendPayeePostRequest(this HttpClient client, AddPayeeRequest model)
        {
            var result = await client.PostAsJsonAsync("/api/Payees", model);
            return result;
        }
    }
}
