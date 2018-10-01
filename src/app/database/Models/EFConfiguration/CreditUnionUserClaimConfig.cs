using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class CreditUnionUserClaimConfig : IEntityTypeConfiguration<CreditUnionUserClaim>
    {
        public void Configure(EntityTypeBuilder<CreditUnionUserClaim> builder)
        {
            builder.ToTable("CreditUnionUserClaims");
        }
    }
}
