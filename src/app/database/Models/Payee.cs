namespace Sans.CreditUnion.Database.Models
{
    public class Payee
    {
        public long Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip5 { get; set; }
        public string Zip4 { get; set; }
        public long MemberIdWhoAdded { get; set; }
    }
}
