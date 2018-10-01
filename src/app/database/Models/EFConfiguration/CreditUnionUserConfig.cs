using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class CreditUnionUserConfig : IEntityTypeConfiguration<CreditUnionUser>
    {
        public void Configure(EntityTypeBuilder<CreditUnionUser> builder)
        {
            builder.ToTable("CreditUnionUsers");

            builder.HasOne(u => u.Member)
                .WithOne(m => m.User)
                .HasForeignKey<CreditUnionUser>(u => u.MemberId);
        }
    }
}
