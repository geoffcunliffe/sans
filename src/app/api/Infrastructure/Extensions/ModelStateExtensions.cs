using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sans.CreditUnion.API.Infrastructure.Models;
using System.Collections.Generic;

namespace Sans.CreditUnion.API
{
    public static class ModelStateExtensions
    {
        public static void AddIdentityErrorsToModelState(this ModelStateDictionary modelState, IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError("", error.Description);
            }
        }

        public static void AddErrorListToModelState(this ModelStateDictionary modelState, List<string> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError("", error);
            }
        }

        public static void AddValidationErrorsToModelState(this ModelStateDictionary modelState, List<ValidationError> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.PropertyName, error.Error);
            }
        }
    }
}
