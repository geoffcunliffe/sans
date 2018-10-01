using System.Collections.Generic;

namespace Sans.CreditUnion.Database.Models
{
    public class Account
    {
        public long Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal InterestRate { get; set; }

        public long MemberId { get; set; }
        public int AccountTypeId { get; set; }

        public List<Transaction> Transactions { get; set; }
        public Member Member { get; set; }
        public AccountType AccountType { get; set; }
    }
}
