using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.BrokerageTrade.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Sans.CreditUnion.Database.Constants;
using Sans.CreditUnion.Database.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.BrokerageTradeTests
{
    public class BrokerageTradeControllerPost : BaseFunctionalTestClass
    {
        public TradeStockRequest ValidModel = new TradeStockRequest
        {
            BrokerageAccountGuid = "123",
            Quantity = 1,
            StockId = BrokerageStocks.BurgerHut.Id,
            TradeType = BrokerageTradeTypes.Buy,
            AccountGuid = SeedData.JohnSmithAccountGuid
        };

        public BrokerageTradeControllerPost(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendBrokerageTradePostRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {
            // Arrange 
            ValidModel.Quantity = 0;

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendBrokerageTradePostRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DoesReturn201_GivenUserIsLoggedInAndViewModelHasAllTheData()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var billPayResult = await Client.SendBrokerageTradePostRequest(ValidModel);

            // Assert
            billPayResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            (await billPayResult.Content.ReadAsAsync<BrokerageTrade>()).Should().NotBeNull();
        }
    }
}
