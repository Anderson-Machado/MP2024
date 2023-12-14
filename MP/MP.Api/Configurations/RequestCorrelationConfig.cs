using CorrelationId;
using CorrelationId.DependencyInjection;
using CorrelationId.Providers;

namespace MP.Api.Configurations
{
    public static class RequestCorrelationConfig
    {
        public static void AddCorrelationIdConfiguration(this IServiceCollection services)
        {
            services.AddCorrelationId<TraceIdCorrelationIdProvider>(options =>
            {
                options.RequestHeader = CorrelationIdOptions.DefaultHeader;
                options.ResponseHeader = CorrelationIdOptions.DefaultHeader;
                options.IncludeInResponse = true;
                options.UpdateTraceIdentifier = true;
                options.AddToLoggingScope = true;
            });
        }

        public static void UseCustomCorrelationId(this IApplicationBuilder app)
        {
            app.UseCorrelationId();
        }
    }
}