using Sans.CreditUnion.API.Infrastructure.Models;
using System.Collections.Generic;

namespace Sans.CreditUnion.API.Features.Members.Models
{
    public class UpdateTravelAbroadResult
    {
        public bool WasSuccessful => ValidationErrors.Count == 0;
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();
    }
}
