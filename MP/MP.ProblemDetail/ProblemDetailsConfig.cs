using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MvcProblemDetailsFactory = Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory;

namespace MP.CrossCutting.ProblemDetail
{
    public static class ProblemDetailsConfig
    {
        public static IServiceCollection AddProblemDetailsConfiguration(this IServiceCollection services)
        {
            services.AddTransient<MvcProblemDetailsFactory, CustomProblemDetailsFactory>();
            services.AddTransient<IValidatorInterceptor, ErrorCodeOnValidationMessageInterceptor>();

            services.ConfigureOptions<ExceptionProblemDetailsCustomOptions>();

            services.ConfigureOptions<ValidationProblemDetailsCustomOptions>();

            return services.AddProblemDetails();
        }

        public static void UseCustomProblemDetails(this IApplicationBuilder app)
        {
            app.UseProblemDetails();
        }
    }
}
