using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sans.CreditUnion.Database.Models;
using Sans.CreditUnion.Database.Models.EFConfiguration;

namespace Sans.CreditUnion.Database.Context
{
    // Have to override the base IdentityDbContext class and give these all concrete implementations in order to override table names
    public class SansCreditUnionDbContext : IdentityDbContext<CreditUnionUser, CreditUnionRole, string, CreditUnionUserClaim, CreditUnionUserRole, CreditUnionUserLogin, CreditUnionRoleClaim, CreditUnionUserToken>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<BillPay> BillPays { get; set; }
        public DbSet<BrokerageAccount> BrokerageAccounts { get; set; }
        public DbSet<BrokerageStockPriceHistory> BrokerageStockPriceHistory { get; set; }
        public DbSet<BrokerageStock> BrokerageStocks { get; set; }
        public DbSet<BrokerageTrade> BrokerageTrades { get; set; }
        public DbSet<CheckOrder> CheckOrders { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public SansCreditUnionDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AccountConfig());
            builder.ApplyConfiguration(new CreditUnionUserConfig());
            builder.ApplyConfiguration(new CreditUnionRoleConfig());
            builder.ApplyConfiguration(new CreditUnionUserRoleConfig());
            builder.ApplyConfiguration(new CreditUnionUserClaimConfig());
            builder.ApplyConfiguration(new CreditUnionUserLoginConfig());
            builder.ApplyConfiguration(new CreditUnionRoleClaimConfig());
            builder.ApplyConfiguration(new CreditUnionUserTokenConfig());
        }
    }
}
