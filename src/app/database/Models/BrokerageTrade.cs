using Sans.CreditUnion.Database.Constants;
using System;

namespace Sans.CreditUnion.Database.Models
{
    public class BrokerageTrade
    {
        public long Id { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public DateTime? PostedDateTime { get; set; }

        public long BrokerageAccountId { get; set; }
        public long StockId { get; set; }
        public long AccountId { get; set; }

        public BrokerageTradeTypes BrokerageTradeType { get; set; }

        public BrokerageStock Stock { get; set; }
        public BrokerageAccount BrokerageAccount { get; set; }
    }
}
