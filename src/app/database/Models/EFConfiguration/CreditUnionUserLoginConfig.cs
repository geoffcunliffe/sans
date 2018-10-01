using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class CreditUnionUserLoginConfig : IEntityTypeConfiguration<CreditUnionUserLogin>
    {
        public void Configure(EntityTypeBuilder<CreditUnionUserLogin> builder)
        {
            builder.ToTable("CreditUnionUserLogins");
        }
    }
}
