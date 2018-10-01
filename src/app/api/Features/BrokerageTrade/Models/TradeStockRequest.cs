using Sans.CreditUnion.Database.Constants;
using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.BrokerageTrade.Models
{
    public class TradeStockRequest
    {
        [Required]
        public string BrokerageAccountGuid { get; set; }

        [Required]
        public long StockId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public BrokerageTradeTypes TradeType { get; set; }

        [Required]
        public string AccountGuid { get; set; }
    }
}
