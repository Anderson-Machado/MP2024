using Flunt.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using MP.CrossCutting.ProblemDetail.Structs;
using System;
using System.Collections.Generic;

namespace MP.CrossCutting.ProblemDetail
{
    /// <summary>
    /// Factory for the creation of <see cref="ProblemDetails"/> and <see cref="ErrorCodeValidationProblemDetails"/>.
    /// <br/>
    /// Our Problem Details responses are a bit different from the default ones from the framework.
    /// For example, on validation error we return both an error message and a error code.
    /// <br/>
    /// <br/>More info on how a Problem Details Factory works:
    /// <br/>https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-5.0
    /// <br/>Based on:
    /// <br/>https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/Infrastructure/DefaultProblemDetailsFactory.cs
    /// </summary>
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        public static ProblemDetails Create(HttpContext httpContext, int statusCode, string? title,
            string? detail, string? type = null, string? instance = null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Type = type,
                Instance = instance
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails);

            return problemDetails;
        }

        public static ProblemDetails Create(HttpContext httpContext)
        {
            return Create(httpContext, httpContext.Response.StatusCode, null, null);
        }

        public override ProblemDetails CreateProblemDetails(HttpContext httpContext,
            int? statusCode = null, string? title = null, string? type = null, string? detail = null,
            string? instance = null)
        {
            return Create(httpContext, statusCode ?? StatusCodes.Status500InternalServerError, title, detail, type,
                instance);
        }

        /// <summary>
        /// ASP.NET Core default validation problem details creator. It shouldn't be used, because the framework's
        /// <see cref="ValidationProblemDetails"/> has a different error structure than our <see cref="ErrorCodeValidationProblemDetails"/>
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        [Obsolete("Use CreateCustomValidationProblemDetails instead")]
#pragma warning disable 0809
        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null,
            string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            throw new NotImplementedException("Use CreateCustomValidationProblemDetails instead");
        }
#pragma warning restore 0809

        public ErrorCodeValidationProblemDetails CreateCustomValidationProblemDetails(
            HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null,
            string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            if (modelStateDictionary == null)
                throw new ArgumentNullException(nameof(modelStateDictionary));

            statusCode ??= StatusCodes.Status400BadRequest;

            var problemDetails = new ErrorCodeValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Instance = instance
            };

            if (title != null)
                problemDetails.Title = title;
            if (detail != null)
                problemDetails.Detail = detail;

            ApplyProblemDetailsDefaults(httpContext, problemDetails);

            return problemDetails;
        }

        public static ErrorCodeValidationProblemDetails CreateCustomValidationProblemDetails(
            HttpContext httpContext, IReadOnlyCollection<Notification> notifications, int? statusCode = null,
            string? title = null, string? type = null, string? detail = null, string? instance = null)
        {
            if (notifications == null)
                throw new ArgumentNullException(nameof(notifications));

            statusCode ??= StatusCodes.Status400BadRequest;

            var problemDetails = new ErrorCodeValidationProblemDetails(notifications)
            {
                Status = statusCode,
                Type = type,
                Instance = instance
            };

            if (title != null)
                problemDetails.Title = title;
            if (detail != null)
                problemDetails.Detail = detail;

            ApplyProblemDetailsDefaults(httpContext, problemDetails);

            return problemDetails;
        }

        public static void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails)
        {
            problemDetails.Type ??= DevPortal.ApiGuideURL;
            problemDetails.Instance ??= httpContext.Request.GetEncodedPathAndQuery();

            problemDetails.Status ??= StatusCodes.Status400BadRequest;
            problemDetails.Title ??= ReasonPhrases.GetReasonPhrase(problemDetails.Status.Value);

            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
        }
    }
}
