using Sans.CreditUnion.API.Features.Authentication.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public static class AuthenticationApiWrapper
    {
        public static async Task<HttpResponseMessage> SendAuthenticationPostRequestAsync(this HttpClient client, string email, string password)
        {
            var model = new AuthenticateRequest
            {
                Email = email,
                Password = password
            };
            
            var result = await client.PostAsJsonAsync("/api/Authentication", model);

            return result;
        }

        public static async Task<string> GetJwtAsync(this HttpClient client, string email, string password)
        {
            var result = await client.SendAuthenticationPostRequestAsync(email, password);
            var authenticateResult = await result.Content.ReadAsAsync<AuthenticateResult>();
            return authenticateResult.Jwt;
        }
    }
}
