using System;
using System.Collections.Generic;

namespace Sans.CreditUnion.Database.Models
{
    public class Member
    {
        public long Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime? TravelingAbroadStart { get; set; }
        public DateTime? TravelingAbroadEnd { get; set; }

        public CreditUnionUser User { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
