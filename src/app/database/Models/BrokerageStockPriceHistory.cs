using System;

namespace Sans.CreditUnion.Database.Models
{
    public class BrokerageStockPriceHistory
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }
        public long StockId { get; set; }
        public BrokerageStock Stock { get; set; }
    }
}
