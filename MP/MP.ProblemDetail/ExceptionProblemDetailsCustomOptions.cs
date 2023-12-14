using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;

namespace MP.CrossCutting.ProblemDetail
{
    public class ExceptionProblemDetailsCustomOptions : IConfigureOptions<ProblemDetailsOptions>
    {
        private readonly IWebHostEnvironment _environment;

        public ExceptionProblemDetailsCustomOptions(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void Configure(ProblemDetailsOptions options)
        {
            options.GetTraceId = httpContext => httpContext.TraceIdentifier;
            options.ContentTypes.Add(ResponseMediaTypes.APPLICATON_PROBLEM_JSON);

            options.MapStatusCode = CustomProblemDetailsFactory.Create;

            options.IncludeExceptionDetails = (context, exception) => !_environment.IsProduction();

            MapExceptions(options);
        }

        private static void MapExceptions(ProblemDetailsOptions options)
        {
            options.Map<Exception>((httpContext, exception) => CustomProblemDetailsFactory.Create(httpContext));
        }
    }
}
