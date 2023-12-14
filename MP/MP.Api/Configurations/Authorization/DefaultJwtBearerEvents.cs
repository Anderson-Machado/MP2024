
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MP.CrossCutting.ProblemDetail;
using MP.CrossCutting.Utils.Resources;
using System.Net;
using System.Text.Json;

namespace MP.Api.Configurations.Authorization
{
    public class DefaultJwtBearerEvents : JwtBearerEvents
    {
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            var logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<DefaultJwtBearerEvents>));
            if (logger is not null && logger is ILogger serilog)
            {
                serilog.LogInformation("Authentication failed: {@Message}", context.Exception?.Message);
            }
            return base.AuthenticationFailed(context);
        }

        public override async Task Challenge(JwtBearerChallengeContext context)
        {
            context.HandleResponse();

            var statusCode = (int)HttpStatusCode.Unauthorized;
            var responseBody = CustomProblemDetailsFactory.Create(
                context.HttpContext,
                statusCode,
                Messages.Unauthorized,
                Messages.InvalidOrExpiredAccessToken
            );
            await WriteResponse(context.Response, responseBody, statusCode);
        }

        private static async Task WriteResponse<T>(HttpResponse response, T responseBody, int statusCode, string contentType = "application/json")
        {
            response.ContentType = contentType;
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonSerializer.Serialize(responseBody, typeof(T)));
        }
    }
}