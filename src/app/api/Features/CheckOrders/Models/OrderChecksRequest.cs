using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.CheckOrders.Models
{
    public class OrderChecksRequest
    {
        [Required]
        public string AccountGuid { get; set; }
    }
}
