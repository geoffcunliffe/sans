using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Sans.CreditUnion.Database.Models;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.AccountTests
{
    public class AccountControllerGet : BaseFunctionalTestClass
    {
        public AccountControllerGet(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendAccountGetRequestAsync();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendAccountGetRequestAsync();
            var accounts = await result.Content.ReadAsAsync<List<Account>>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            accounts.Count.Should().Be(1);
            accounts[0].Guid.Should().Be(SeedData.JohnSmithAccountGuid);
        }
    }
}
