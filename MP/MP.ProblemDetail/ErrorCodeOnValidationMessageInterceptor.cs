
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace MP.CrossCutting.ProblemDetail
{
    public class ErrorCodeOnValidationMessageInterceptor : IValidatorInterceptor
    {
        public const string ERROR_CODE_SEPARATOR = "_@_";

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext; // NOOP
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext,
            ValidationResult result)
        {
            result.Errors.ForEach(err => err.ErrorMessage = $"{err.ErrorCode}{ERROR_CODE_SEPARATOR}{err.ErrorMessage}");
            return result;
        }


    }
}
