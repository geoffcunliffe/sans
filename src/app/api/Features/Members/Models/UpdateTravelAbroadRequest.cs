using System;
using System.ComponentModel.DataAnnotations;

namespace Sans.CreditUnion.API.Features.Members.Models
{
    public class UpdateTravelAbroadRequest
    {
        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}
