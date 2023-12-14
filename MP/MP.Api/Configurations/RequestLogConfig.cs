using Serilog;
using Serilog.Events;

namespace MP.Api.Configurations
{
    public static class RequestLogConfig
    {
        private const string MESSAGE_TEMPLATE =
            "HTTP {RequestMethod} to '{Path}' responded with {StatusCode} in {Elapsed:0} ms";

        public static void UseRequestLogConfiguration(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = MESSAGE_TEMPLATE;
                options.GetLevel = RequestLogLevel;
                options.EnrichDiagnosticContext = EnrichRequest;
            });
        }

        private static LogEventLevel RequestLogLevel(HttpContext httpCtx, double elapsedMs, Exception? ex)
        {
            if (ex != null || httpCtx.Response.StatusCode >= 500)
                return LogEventLevel.Error;

            if (httpCtx.Request.Method == "OPTIONS" ||
                httpCtx.Request.Path == "/service-worker.js" ||
                httpCtx.Request.Path == "/favicon.ico")
                return LogEventLevel.Verbose;

            if (IsHealthCheckEndpoint(httpCtx))
                return LogEventLevel.Debug;

            return LogEventLevel.Information;
        }

        private static void EnrichRequest(IDiagnosticContext diagnosticCtx, HttpContext httpCtx)
        {
            diagnosticCtx.Set("ClientIP", httpCtx.Connection.RemoteIpAddress?.ToString() ?? "");
            diagnosticCtx.Set("User", httpCtx.User.Identity?.Name ?? "");

            if (httpCtx.Request.Path.HasValue)
                diagnosticCtx.Set("Path", httpCtx.Request.Path.Value);

            if (httpCtx.Request.QueryString.HasValue)
                diagnosticCtx.Set("QueryString", httpCtx.Request.QueryString);

            var endpoint = httpCtx.GetEndpoint();
            if (endpoint != null)
                diagnosticCtx.Set("EndpointName", endpoint.DisplayName);
        }

        private static bool IsHealthCheckEndpoint(HttpContext ctx)
        {
            const string defaultHealthCheckDisplayName = "Health checks"; // Defined by the framework

            var endpoint = ctx.GetEndpoint();
            if (endpoint != null)
                return string.Equals(endpoint.DisplayName, defaultHealthCheckDisplayName, StringComparison.Ordinal);

            // No endpoint, so not a health check endpoint
            return false;
        }
    }
}