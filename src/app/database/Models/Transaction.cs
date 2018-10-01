using System;

namespace Sans.CreditUnion.Database.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public long ReceivingAccountId { get; set; }
        public DateTime TransactionDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? PostedDateTime { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public long SendingAccountId { get; set; }
        public string Status => PostedDateTime.HasValue ? "Posted" : "Pending";
        public Account Account { get; set; }
    }
}
