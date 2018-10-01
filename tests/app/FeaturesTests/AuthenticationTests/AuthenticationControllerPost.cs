using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.Authentication.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.AuthenticationTests
{
    public class AuthenticationControllerPost : BaseFunctionalTestClass
    {
        public AuthenticationControllerPost(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenEmailIsNull()
        {
            // Act
            var result = await Client.SendAuthenticationPostRequestAsync(null, SeedData.EveryonesPassword);
            var authenticateResult = await result.Content.ReadAsAsync<AuthenticateResult>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            authenticateResult.Jwt.Should().BeNull();
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenPasswordIsNull()
        {
            // Act
            var result = await Client.SendAuthenticationPostRequestAsync(SeedData.JohnSmithEmail, null);
            var authenticateResult = await result.Content.ReadAsAsync<AuthenticateResult>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            authenticateResult.Jwt.Should().BeNull();
        }

        [Fact]
        public async Task DoesBadRequestWithNoJwt_GivenUserLogsInWithIncorrectPassword()
        {
            // Act
            var result = await Client.SendAuthenticationPostRequestAsync(SeedData.JohnSmithEmail, "WrongPassword!");
            var authenticateResult = await result.Content.ReadAsAsync<AuthenticateResult>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            authenticateResult.Jwt.Should().BeNull();
        }

        [Fact]
        public async Task DoesReturnJwt_GivenUserLogsInWithCorrectPassword()
        {
            var result = await Client.SendAuthenticationPostRequestAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);

            var authenticateResult = await result.Content.ReadAsAsync<AuthenticateResult>();
            authenticateResult.Jwt.Should().NotBeNull();

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(authenticateResult.Jwt);
            jwt.Should().NotBeNull();
        }
    }
}