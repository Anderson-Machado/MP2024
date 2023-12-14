namespace MP.Api.Configurations.Swagger
{
    public static class SwaggerConfig
    {
        public static void AddCustomSwaggerConfiguration(this IServiceCollection services,
            IWebHostEnvironment environment)
        {
            if (environment.IsProduction())
                return;

            // Pega as informações do appsettings através da classe SwaggerSettings
            services.AddOptions<SwaggerSettings>().BindConfiguration(SwaggerSettings.SETTINGS_KEY);

            services.ConfigureOptions<SwaggerDocGeneratorOptions>(); // Configura geração de documento do swagger [AddSwaggerGen()]
            services.ConfigureOptions<SwaggerUiOptions>(); // Configura UI do swagger [UseSwaggerUI()]

            services.AddSwaggerGen();
        }

        public static void UseCustomSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}