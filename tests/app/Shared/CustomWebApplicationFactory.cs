using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sans.CreditUnion.API.Infrastructure.Constants;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.Database.Context;
using Sans.CreditUnion.Database.Models;

namespace Sans.CreditUnion.API.Tests.Shared
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<SansCreditUnionDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                services.AddIdentityCore<CreditUnionUser>(options => options = OptionConstants.IdentityOptions)
                    .AddEntityFrameworkStores<SansCreditUnionDbContext>()
                    .AddRoles<CreditUnionRole>()
                    .AddRoleValidator<RoleValidator<CreditUnionRole>>()
                    .AddRoleManager<RoleManager<CreditUnionRole>>()
                    .AddSignInManager<SignInManager<CreditUnionUser>>();

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<SansCreditUnionDbContext>();
                    dbContext.Seed(scopedServices);
                }
            });
        }
    }
}
