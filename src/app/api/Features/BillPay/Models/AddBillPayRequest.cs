using System;
using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.BillPay.Models
{
    public class AddBillPayRequest
    {
        public bool IsRecurring { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        [Required]
        public string PayeeGuid { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public DateTime? FirstPaymentDate { get; set; }
    }
}
