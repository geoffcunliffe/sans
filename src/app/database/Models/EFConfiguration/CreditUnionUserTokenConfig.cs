using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class CreditUnionUserTokenConfig : IEntityTypeConfiguration<CreditUnionUserToken>
    {
        public void Configure(EntityTypeBuilder<CreditUnionUserToken> builder)
        {
            builder.ToTable("CreditUnionUserTokens");
        }
    }
}
