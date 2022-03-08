using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Delta.InvoicingAngular
{
    public static class ValidationExceptionExtension
    {
        public static ModelStateDictionary ToModelState(this ValidationException ex)
        {
            if (ex == null) throw new ArgumentNullException(nameof(ex));

            var result = new ModelStateDictionary();

            foreach (ValidationFailure failure in ex.Errors)
            {
                result.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }

            return result;
        }
    }
}
