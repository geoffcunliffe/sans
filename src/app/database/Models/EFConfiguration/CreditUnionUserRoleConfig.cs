using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class CreditUnionUserRoleConfig : IEntityTypeConfiguration<CreditUnionUserRole>
    {
        public void Configure(EntityTypeBuilder<CreditUnionUserRole> builder)
        {
            builder.ToTable("CreditUnionUserRoles");
        }
    }
}
