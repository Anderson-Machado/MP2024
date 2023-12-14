using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MP.CrossCutting.Utils.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace MP.Api.Configurations.Authorization
{
    public static class AuthorizationConfig
    {
        public static ControllerActionEndpointConventionBuilder BypassAuthOnLocalEnvironment(
            this ControllerActionEndpointConventionBuilder builder, IWebHostEnvironment environment)
        {
            if (environment.IsLocal())
                builder.AllowAnonymous();

            return builder;
        }

        public static void AddAuthConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    // Todo: Remover linha abaixo, usada momentaneamente para ignorar assinatura do token
                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);

                        return jwt;
                    },
                    ClockSkew = TimeSpan.Zero
                };

                x.Events = new DefaultJwtBearerEvents();
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });
        }

        public static void UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}