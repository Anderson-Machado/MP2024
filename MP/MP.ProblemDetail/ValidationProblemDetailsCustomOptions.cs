using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace MP.CrossCutting.ProblemDetail
{
    public class ValidationProblemDetailsCustomOptions : IConfigureOptions<ApiBehaviorOptions>
    {
        public void Configure(ApiBehaviorOptions options)
        {
            // Supress the framework's default client error (4xx status) mapping, so we can do our own
            options.SuppressMapClientErrors = true;

            // Handles the creation of a controller result whenever there's a validation error
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                if (!(problemDetailsFactory is CustomProblemDetailsFactory factory))
                    throw new InvalidOperationException("Wrong ProblemDetailsFactory registered. Should be CustomProblemDetailsFactory");

                var problemDetails = factory.CreateCustomValidationProblemDetails(context.HttpContext, context.ModelState);
                var result = new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status
                };

                result.ContentTypes.Add(ResponseMediaTypes.APPLICATON_PROBLEM_JSON);
                return result;
            };
        }
    }
}
