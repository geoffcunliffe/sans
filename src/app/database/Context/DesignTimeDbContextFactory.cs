using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Sans.CreditUnion.Database.Context
{
    // Since there's no ASP.NET Core app attached to this yet, we need a IDesignTimeDbContextFactory in order to run migrations
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SansCreditUnionDbContext>
    {
        public SansCreditUnionDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("sans-creditunion-database-0910fd34-6abc-1d9e-p12k-5e4ege5jkl0c")
                .Build();

            var builder = new DbContextOptionsBuilder<SansCreditUnionDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseMySql(connectionString);

            return new SansCreditUnionDbContext(builder.Options);
        }
    }
}
