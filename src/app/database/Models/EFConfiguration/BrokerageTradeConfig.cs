using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sans.CreditUnion.Database.Constants;
using System;

namespace Sans.CreditUnion.Database.Models.EFConfiguration
{
    public class BrokerageTradeConfig : IEntityTypeConfiguration<BrokerageTrade>
    {
        public void Configure(EntityTypeBuilder<BrokerageTrade> builder)
        {
            builder.Property(e => e.BrokerageTradeType)
               .HasConversion(
                   v => v.ToString(),
                   v => (BrokerageTradeTypes)Enum.Parse(typeof(BrokerageTradeTypes), v));
        }
    }
}
