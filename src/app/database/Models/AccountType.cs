namespace Sans.CreditUnion.Database.Models
{
    // Possible Account Types: Checking, Savings, CD, etc.
    public class AccountType
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
