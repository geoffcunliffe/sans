using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Sans.CreditUnion.Database.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.TransactionsTests
{
    public class TransactionsControllerGet : BaseFunctionalTestClass
    {
        public TransactionsControllerGet(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesNotReturnData_GivenUserIsLoggedIn()
        {
            // Act
            var result = await Client.SendTransactionsGetRequestAsync();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnData_GivenUserIsLoggedIn()
        {
            // Arrange
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            // Act
            var result = await Client.SendTransactionsGetRequestAsync();
            var transactions = await result.Content.ReadAsAsync<List<Transaction>>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            transactions.Should().NotBeNull();
        }
    }
}
