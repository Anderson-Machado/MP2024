using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MP.Api.Configurations.Swagger
{
    public class SwaggerUiOptions : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly SwaggerSettings _swaggerSettings;

        public SwaggerUiOptions(IOptions<SwaggerSettings> swaggerUiConfig)
        {
            _swaggerSettings = swaggerUiConfig.Value;
        }

        public void Configure(SwaggerUIOptions options)
        {
            string version = _swaggerSettings.Version;

            options.SwaggerEndpoint(
                url: $"/swagger/{version}/swagger.json",
                name: $"{_swaggerSettings.Title} {_swaggerSettings.Version}"
            );
        }
    }
}