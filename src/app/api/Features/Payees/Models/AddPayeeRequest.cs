using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.Payees.Models
{
    public class AddPayeeRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip5 { get; set; }

        public string Zip4 { get; set; }
    }
}
