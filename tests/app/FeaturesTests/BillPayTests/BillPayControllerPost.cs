using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.BillPay.Models;
using Sans.CreditUnion.API.Features.Payees.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Sans.CreditUnion.Database.Constants;
using Sans.CreditUnion.Database.Models;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.BillPayTests
{
    public class BillPayControllerPost : BaseFunctionalTestClass
    {
        public AddBillPayRequest ValidBillPayModel = new AddBillPayRequest
        {
            Amount = 100,
            FirstPaymentDate = DateTime.Now,
            Frequency = BillPayFrequencies.Monthly,
            IsRecurring = true
        };

        public AddPayeeRequest ValidPayeeModel = new AddPayeeRequest
        {
            City = "Des Moines",
            Name = "Contoso",
            State = "IA",
            Street1 = "Street",
            Zip5 = "50004"
        };

        public BillPayControllerPost(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendBillPayPostRequest(ValidPayeeModel, ValidBillPayModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {
            // Arrange 
            ValidBillPayModel.Amount = null;

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var result = await Client.SendBillPayPostRequest(ValidPayeeModel, ValidBillPayModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DoesReturn201_GivenUserIsLoggedInAndViewModelHasAllTheData()
        {
            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            var billPayResult = await Client.SendBillPayPostRequest(ValidPayeeModel, ValidBillPayModel);
            
            // Assert
            billPayResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            (await billPayResult.Content.ReadAsAsync<BillPay>()).Should().NotBeNull();
        }
    }
}
