using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sans.CreditUnion.API.Features.Authentication.Models;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.API.Tests.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sans.CreditUnion.API.Tests.SecurityTests
{
    public class HighRiskCodeTest
    {
        [Theory]
        [InlineData("../../../../../src/app/api/Features/Authentication/AuthenticationController.cs", "2c79238c6af087c41bcc912c7cca9ac315fa277f")]
        public void HighRiskCode_CheckSumTest(string file, string checksum)
        {
            string actual = HashWrapper.GetChecksum(file);
            actual.Should().Be(checksum);
        }
    }
}