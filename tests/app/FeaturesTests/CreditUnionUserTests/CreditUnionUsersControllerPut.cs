using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.CreditUnionUsers.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.CreditUnionUserTests
{
    public class CreditUnionUsersControllerPut : BaseFunctionalTestClass
    {
        public UpdateUserRequest ValidModel = new UpdateUserRequest
        {
            Email = SeedData.JohnSmithEmail,
            PhoneNumber = "1111111111"
        };

        public CreditUnionUsersControllerPut(CustomWebApplicationFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendCreditUnionUsersPutRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {
            // Arrange 
            ValidModel.Email = null;

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendCreditUnionUsersPutRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DoesReturnOk_GivenRequestIsValid()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendCreditUnionUsersPutRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
