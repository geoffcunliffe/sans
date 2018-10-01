using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.BillPay.Models;
using Sans.CreditUnion.API.Features.Payees.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using Sans.CreditUnion.Database.Constants;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.PayeesTests
{
    public class BillPayControllerDelete : BaseFunctionalTestClass
    {
        public BillPayControllerDelete(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DoesReturn401Unauthorized_GivenUserIsNotLoggedIn()
        {
            // Act
            var result = await Client.SendBillPayDeleteRequest(Guid.NewGuid().ToString());

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public async Task DoesReturn200_GivenUserIsLoggedIn()
        {
            // Arrange
            var payeeModel = new AddPayeeRequest
            {
                City = "Des Moines",
                Name = "Contoso",
                State = "IA",
                Street1 = "Street",
                Zip5 = "50004"
            };

            var billPayModel = new AddBillPayRequest
            {
                Amount = 100,
                FirstPaymentDate = DateTime.Now,
                Frequency = BillPayFrequencies.Monthly,
                IsRecurring = true
            };

            // Act
            string jwt = await Client.GetJwtAsync(SeedData.JohnSmithEmail, SeedData.EveryonesPassword);
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");

            // Create Bill Pay
            var createBillPayResult = await Client.SendBillPayPostRequest(payeeModel, billPayModel);
            var billPay = await createBillPayResult.Content.ReadAsAsync<BillPay>();

            // Now Delete It
            var result = await Client.SendBillPayDeleteRequest(billPay.Guid);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
