using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class AccountConfig : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasMany(a => a.Transactions)
                .WithOne(t => t.Account);

            builder.HasOne(a => a.Member)
                .WithMany(m => m.Accounts)
                .HasForeignKey(a => a.MemberId);
        }
    }
}
