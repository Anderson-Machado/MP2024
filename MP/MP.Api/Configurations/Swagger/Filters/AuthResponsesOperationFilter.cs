using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace MP.Api.Configurations.Swagger.Filters
{
    /// <summary>
    /// Adds ProduceResponseType for 401 (Unauthorized) and 403 (Forbidden) automatically for every endpoint
    /// that requires auth.
    /// </summary>
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!RequiresAuth(context))
                return;

            string defaultProblemResponseClass = nameof(ProblemDetails);

            AddResponseTypeForStatusCode(operation, HttpStatusCode.Unauthorized, ResponseMediaTypes.APPLICATON_PROBLEM_JSON, defaultProblemResponseClass);
            AddResponseTypeForStatusCode(operation, HttpStatusCode.Forbidden, ResponseMediaTypes.APPLICATON_PROBLEM_JSON, defaultProblemResponseClass);
        }

        private static bool RequiresAuth(OperationFilterContext context)
        {
            bool hasAuthRequirement = GetControllerAndActionAttributes<AuthorizeAttribute>(context).Any();
            bool allowsToBypassAuthRequirement = GetControllerAndActionAttributes<AllowAnonymousAttribute>(context).Any();

            return hasAuthRequirement && !allowsToBypassAuthRequirement;
        }

        private static IList<T> GetControllerAndActionAttributes<T>(OperationFilterContext context)
            where T : Attribute
        {
            return context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<T>().ToList();
        }

        private static void AddResponseTypeForStatusCode(OpenApiOperation operation, HttpStatusCode status, string mediaType, string responseClassName)
        {
            string statusCodeNumber = ((int)status).ToString();
            if (operation.Responses.ContainsKey(statusCodeNumber)) // Response type for status exists already
                return;

            operation.Responses.Add(statusCodeNumber, new OpenApiResponse
            {
                Description = status.GetDisplayName(),
                Content = GenerateOpenApiResponseContent(mediaType, responseClassName)
            });
        }

        private static Dictionary<string, OpenApiMediaType> GenerateOpenApiResponseContent(string mediaType, string responseClassName)
        {
            return new Dictionary<string, OpenApiMediaType>
            {
                {
                    mediaType,
                    new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Reference = new OpenApiReference
                            {
                                Id = responseClassName,
                                Type = ReferenceType.Schema
                            }
                        }
                    }
                }
            };
        }
    }
}