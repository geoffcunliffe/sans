namespace Sans.CreditUnion.Database.Models
{
    public class BrokerageAccount
    {
        public long Id { get; set; }

        public string Guid { get; set; } = System.Guid.NewGuid().ToString();

        public long MemberId { get; set; }

        public Member Member { get; set; }
    }
}
