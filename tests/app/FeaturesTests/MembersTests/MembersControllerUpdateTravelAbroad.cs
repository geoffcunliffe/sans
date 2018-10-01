using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.Members.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.MembersTests
{
    public class MembersControllerUpdateTravelAbroad : BaseFunctionalTestClass
    {
        public UpdateTravelAbroadRequest ValidModel = new UpdateTravelAbroadRequest
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7)
        };

        public MembersControllerUpdateTravelAbroad(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendTravelAbroadPutRequestAsync(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {
            // Arrange 
            ValidModel.EndDate = ValidModel.StartDate.Value.AddDays(-1);

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendTravelAbroadPutRequestAsync(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DoesReturnOk_GivenRequestIsValid()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendTravelAbroadPutRequestAsync(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
