using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class CreditUnionRoleConfig : IEntityTypeConfiguration<CreditUnionRole>
    {
        public void Configure(EntityTypeBuilder<CreditUnionRole> builder)
        {
            builder.ToTable("CreditUnionRoles");
        }
    }
}
