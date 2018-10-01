using System.Collections.Generic;

namespace Sans.CreditUnion.Database.Models
{
    public class BrokerageStock
    {
        public long Id { get; set; }
        public string Ticker { get; set; }
        public string CompanyName { get; set; }
        public decimal PreviousDayClose { get; set; }

        public List<BrokerageStockPriceHistory> PriceHistories { get; set; }
    }
}
