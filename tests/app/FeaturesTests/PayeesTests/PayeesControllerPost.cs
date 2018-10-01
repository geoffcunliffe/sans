using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.Payees.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.PayeesTests
{
    public class PayeesControllerPost : BaseFunctionalTestClass
    {
        public AddPayeeRequest ValidModel = new AddPayeeRequest
        {
            City = "Des Moines",
            Name = "Contoso",
            State = "IA",
            Street1 = "Street",
            Zip5 = "50004"
        };

        public PayeesControllerPost(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendPayeePostRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {
            // Arrange 
            ValidModel.Name = null;

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendPayeePostRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DoesReturn201_GivenUserIsLoggedIn()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
            var result = await Client.SendPayeePostRequest(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
    }
}
