using System;

namespace Sans.CreditUnion.Database.Models
{
    public class BillPay
    {
        public long Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public long MemberId { get; set; }
        public long PayeeId { get; set; }
        public bool IsRecurring { get; set; }
        public string Frequency { get; set; }
        public DateTime? NextPaymentDate { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public decimal Amount { get; set; }

        public Payee Payee { get; set; }
        public Member Member { get; set; }
    }
}
