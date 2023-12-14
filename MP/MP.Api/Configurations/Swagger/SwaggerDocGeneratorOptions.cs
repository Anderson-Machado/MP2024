using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MP.Api.Configurations.Swagger.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MP.Api.Configurations.Swagger
{
    public class SwaggerDocGeneratorOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly SwaggerSettings _swaggerSettings;

        public SwaggerDocGeneratorOptions(IOptions<SwaggerSettings> swaggerUiConfig)
        {
            _swaggerSettings = swaggerUiConfig.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.IncludeXmlComments(GenerateXmlCommentsFilePath());

            options.SwaggerDoc(_swaggerSettings.Version, new OpenApiInfo
            {
                Title = _swaggerSettings.Title,
                Description = _swaggerSettings.Description,
                Version = _swaggerSettings.Version,
                //Contact = new OpenApiContact
                //{
                //    Name = _swaggerSettings.ContactName,
                //    Url = new Uri(_swaggerSettings.ContactUrl ?? "")
                //}
            });

            TagActionsByGroupOrController(options);

            options.UseAllOfToExtendReferenceSchemas(); // Permite definir enum com valor default no swagger

            AddAuthorizationTokenButton(options);

            options.OperationFilter<AuthResponsesOperationFilter>();

        }

        private static void AddAuthorizationTokenButton(SwaggerGenOptions options)
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme.ToLower(), // Must be lowercase
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer token **__only__**",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            };

            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        securityScheme, Array.Empty<string>()
                    }
                });
        }

        private static string GenerateXmlCommentsFilePath()
        {
            /*
             * Esse arquivo XML usado p/ documentação do Swagger UI é gerado pela solução por meio do
             * GenerateDocumentationFile no csproj, que por padrão recebe o nome do assembly
             */
            Assembly startupAssembly = Assembly.GetEntryAssembly()!;

            if (startupAssembly == null)
                throw new NullReferenceException("Assembly do Startup não encontrado");

            string basePath = AppContext.BaseDirectory;
            string fileName = startupAssembly.GetName().Name + ".xml";

            string path = Path.Combine(basePath, fileName);

            if (!File.Exists(path))
                throw new InvalidOperationException("Arquivo XML de documentação do Swagger UI não encontrado. " +
                    "Verifique se o swagger foi configurado no '.csproj' do projeto de Startup.");

            return path;
        }

        /// <summary>
        /// Define que as actions serão agrupadas no swagger UI por GroupName se disponível, com fallback para o padrão
        /// de Controller Name
        /// </summary>
        private static void TagActionsByGroupOrController(SwaggerGenOptions swaggerGenOptions)
        {
            const string aspnetDefaultControllerRouteValue = "controller";

            swaggerGenOptions.TagActionsBy(description => string.IsNullOrEmpty(description.GroupName)
                ? new[] { description.ActionDescriptor.RouteValues[aspnetDefaultControllerRouteValue] }
                : new[] { description.GroupName }
            );

            /*
             * Por padrão, o DocInclusionPredicate remove actions cujo GroupName seja alterado. Como o GroupName
             * será usado para agrupar actions na UI, removemos esse comportamento.
             */
            swaggerGenOptions.DocInclusionPredicate((_, _) => true);
        }
    }
}