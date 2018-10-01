using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.Members.Models;
using Sans.CreditUnion.API.Tests.Shared;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests.MembersTests
{
    public class MembersControllerPost : BaseFunctionalTestClass
    {
        public CreateMemberRequest ValidModel = new CreateMemberRequest
        {
            City = "Des Moines",
            Email = "sanscreditunionjanedoe@gmail.com",
            FirstName = "Jane",
            LastName = "Doe",
            Password = "Test1234!",
            State = "IA",
            Street = "Street",
            ZipCode = "50004"
        };

        public MembersControllerPost(CustomWebApplicationFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task DoesReturnBadRequest_GivenValidationDoesNotPass()
        {
            // Arrange 
            ValidModel.LastName = null;

            // Act
            var result = await Client.SendMembersPostRequestAsync(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task DoesReturnOk_GivenRequestIsValid()
        {
            // Act
            var result = await Client.SendMembersPostRequestAsync(ValidModel);

            // Assert
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
        }
    }
}
