using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sans.CreditUnion.API.Infrastructure.Startup;
using Sans.CreditUnion.Database.Context;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Sans.CreditUnion.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SansCreditUnionDbContext>();

                    if (context.Database.GetPendingMigrations().Any())
                        context.Database.Migrate();

                    context.Seed(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {
                    options.AddServerHeader = false;
                })
                .UseUrls("https://*:44300");
    }
}
