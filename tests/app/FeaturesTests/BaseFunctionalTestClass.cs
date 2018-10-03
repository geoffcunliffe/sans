using Microsoft.AspNetCore.Mvc.Testing;
using Sans.CreditUnion.API.Tests.Shared;
using System.Net.Http;
using Xunit;

namespace Sans.CreditUnion.API.Tests.FeaturesTests
{
    public abstract class BaseFunctionalTestClass : IClassFixture<CustomWebApplicationFactory>
    {
        protected HttpClient Client;
        protected CustomWebApplicationFactory Factory { get; }

        public BaseFunctionalTestClass(CustomWebApplicationFactory factory)
        {
            Factory = factory;
            Client = Factory.CreateClient();
        }
    }
}
