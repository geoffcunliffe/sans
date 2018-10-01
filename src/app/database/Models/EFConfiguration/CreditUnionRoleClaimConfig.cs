using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class CreditUnionRoleClaimConfig : IEntityTypeConfiguration<CreditUnionRoleClaim>
    {
        public void Configure(EntityTypeBuilder<CreditUnionRoleClaim> builder)
        {
            builder.ToTable("CreditUnionRoleClaims");
        }
    }
}
