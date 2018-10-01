using System;

namespace Sans.CreditUnion.Database.Models
{
    public class CheckOrder
    {
        public long Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public long AccountId { get; set; }
        public DateTime OrderedDate { get; set; } = DateTime.UtcNow;

        public Account Account { get; set; }
    }
}
