using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.Transactions.Models
{
    public class TransferFundsRequest
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string SenderUserId { get; set; }

        // Use Email for the Receiver to do something like Paypal where you type in someone's email to transfer them funds
        [Required]
        public string ReceiverEmail { get; set; }

        [Required]
        public string Description { get; set; }
    }
}