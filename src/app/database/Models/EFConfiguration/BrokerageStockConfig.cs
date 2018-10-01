using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class BrokerageStockConfig : IEntityTypeConfiguration<BrokerageStock>
    {
        public void Configure(EntityTypeBuilder<BrokerageStock> builder)
        {
            builder.HasMany(s => s.PriceHistories)
                .WithOne(p => p.Stock)
                .HasForeignKey(p => p.StockId);
        }
    }
}
