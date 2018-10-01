using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.CheckOrders.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Sans.CreditUnion.Database.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.CheckOrdersTests
{
    public class CheckOrdersControllerPost : BaseFunctionalTestClass
    {
        public OrderChecksRequest ValidModel = new OrderChecksRequest
        {
            AccountGuid = SeedData.JohnSmithAccountGuid
        };

        public CheckOrdersControllerPost(CustomWebApplicationFactory factory) : base (factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendCheckOrdersPostRequestAsync(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {
            // Arrange 
            ValidModel.AccountGuid = null;

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendCheckOrdersPostRequestAsync(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DoesReturn201_GivenUserIsLoggedInAndViewModelHasAllTheData()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var checkOrderResult = await Client.SendCheckOrdersPostRequestAsync(ValidModel);

            // Assert
            checkOrderResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            (await checkOrderResult.Content.ReadAsAsync<CheckOrder>()).Should().NotBeNull();
        }
    }
}
