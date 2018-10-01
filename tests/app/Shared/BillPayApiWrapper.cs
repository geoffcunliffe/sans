using Sans.CreditUnion.API.Features.BillPay.Models;
using Sans.CreditUnion.API.Features.Payees.Models;
using Sans.CreditUnion.Database.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class BillPayApiWrapper
    {
        public const string Base = "/api/BillPay";
        public static async Task<HttpResponseMessage> SendBillPayPostRequest(this HttpClient client, AddPayeeRequest payeeModel, AddBillPayRequest billPayModel)
        {
            // First add the Payee
            var payeeResult = await client.SendPayeePostRequest(payeeModel);
            var payee = await payeeResult.Content.ReadAsAsync<Payee>();

            billPayModel.PayeeGuid = payee?.Guid;

            // Then create the BillPay
            var result = await client.PostAsJsonAsync(Base, billPayModel);
            return result;
        }

        public static async Task<HttpResponseMessage> SendBillPayDeleteRequest(this HttpClient client, string guid)
        {
            var result = await client.DeleteAsync($"{Base}/{guid}");
            return result;
        }
    }
}
