using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.CrossCutting.ProblemDetail
{
    public class ErrorCodeValidationProblemDetails : ProblemDetails
    {
        public ErrorCodeValidationProblemDetails(ModelStateDictionary modelState)
        {
            if (modelState == null)
                throw new ArgumentNullException(nameof(modelState));

            Title = Messages.ProblemDetailsTitle;
            Detail = Messages.ProblemDetailsDetail;

            base.Extensions["errors"] = MapModelStateToValidationErrors(modelState);
        }

        public ErrorCodeValidationProblemDetails(IReadOnlyCollection<Notification> notifications)
        {
            if (notifications == null)
                throw new ArgumentNullException(nameof(notifications));

            Title = Messages.ProblemDetailsTitle;
            Detail = Messages.ProblemDetailsDetail;

            base.Extensions["errors"] = MapModelStateToValidationErrors(notifications);
        }

        private static Dictionary<string, ValidationProblemDetailsError[]> MapModelStateToValidationErrors(
            IReadOnlyCollection<Notification> notifications)
        {
            return notifications.ToDictionary(
                x => x.Key,
                y => notifications.Select(x => x)
                    .Where(x => x.Key.Equals(y.Key))
                    .Select(x => MapIndividualModelStateError(x.Message))
                    .ToArray()
                );
        }

        private static Dictionary<string, ValidationProblemDetailsError[]> MapModelStateToValidationErrors(
            ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, ValidationProblemDetailsError[]>();
            foreach (var keyModelStatePair in modelState)
            {
                var (key, value) = keyModelStatePair;
                errors.Add(key, value.Errors.Select(MapIndividualModelStateError).ToArray());
            }

            return errors;
        }

        private static ValidationProblemDetailsError MapIndividualModelStateError(string errorMessage)
        {
            if (string.IsNullOrEmpty(errorMessage))
                return new ValidationProblemDetailsError("ELTMXXXXX", "Error");

            // Since ModelState doesn't have a way to pass ErrorCode, it was added at the start of the error message
            // to be retrieved here
            string[] splitErrCodeAndErrMsg = errorMessage.Split(ErrorCodeOnValidationMessageInterceptor.ERROR_CODE_SEPARATOR);
            if (splitErrCodeAndErrMsg.Length == 1)
                return new ValidationProblemDetailsError("ELTMXXXXX", errorMessage);

            return new ValidationProblemDetailsError(
                errorCode: splitErrCodeAndErrMsg[0],
                description: splitErrCodeAndErrMsg[1]
            );
        }

        private static ValidationProblemDetailsError MapIndividualModelStateError(ModelError error)
        {
            if (string.IsNullOrEmpty(error.ErrorMessage))
                return new ValidationProblemDetailsError("ELTMXXXXX", "Error");

            // Since ModelState doesn't have a way to pass ErrorCode, it was added at the start of the error message
            // to be retrieved here
            string[] splitErrCodeAndErrMsg = error.ErrorMessage.Split(ErrorCodeOnValidationMessageInterceptor.ERROR_CODE_SEPARATOR);
            if (splitErrCodeAndErrMsg.Length == 1)
                return new ValidationProblemDetailsError("ELTMXXXXX", error.ErrorMessage);

            return new ValidationProblemDetailsError(
                errorCode: splitErrCodeAndErrMsg[0],
                description: splitErrCodeAndErrMsg[1]
            );
        }
    }
}
