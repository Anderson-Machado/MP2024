using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MP.Api.Configurations.HealthCheck
{
    public static class HealthCheckConfig
    {
        /// <summary> Tag para health checks que definem se aplicação finalizou seu startup </summary>
        private const string STARTUP_TAG = "startup";

        /// <summary> Tag para health checks que definem se a aplicação deve ser reiniciada </summary>
        private const string LIVE_TAG = "live";

        /// <summary> Tag para health checks que definem se a aplicação deve receber tráfego </summary>
        private const string READY_TAG = "ready";

        public static void AddHealthCheckConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnStr = configuration.GetConnectionString("DbConnection");

            services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { LIVE_TAG, STARTUP_TAG })
                .AddSqlServer(dbConnStr, tags: new[] { READY_TAG, STARTUP_TAG })
                .AddCheck<CacheHealthCheck>(string.Empty, tags: new[] { STARTUP_TAG });
        }

        public static void MapHealthCheckConfiguration(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/startup", new HealthCheckOptions()
            {
                Predicate = x => x.Tags.Contains(STARTUP_TAG),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = x => x.Tags.Contains(LIVE_TAG),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = x => x.Tags.Contains(READY_TAG),
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}