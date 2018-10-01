using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Sans.CreditUnion.Database.Constants;
using Sans.CreditUnion.Database.Models;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.BrokerageStocksTests
{
    public class BrokerageStocksControllerGet : BaseFunctionalTestClass
    {
        public BrokerageStocksControllerGet(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendBrokerageStocksGetRequest();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnSingleStock_GivenUserIsLoggedIn()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendBrokerageStocksGetRequest(BrokerageStocks.BurgerHut.Id);
            var stock = await result.Content.ReadAsAsync<BrokerageStock>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            stock.CompanyName.Should().Be(BrokerageStocks.BurgerHut.CompanyName);
        }

        [Fact]
        public async Task DoesReturnStocks_GivenUserIsLoggedIn()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendBrokerageStocksGetRequest();
            var stocks = await result.Content.ReadAsAsync<List<BrokerageStock>>();

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            stocks.Any(s => s.Id == BrokerageStocks.BurgerHut.Id).Should().BeTrue();
            stocks.Any(s => s.Id == BrokerageStocks.FurnitureRUs.Id).Should().BeTrue();
            stocks.Any(s => s.Id == BrokerageStocks.SansCreditUnion.Id).Should().BeTrue();
        }
    }
}
