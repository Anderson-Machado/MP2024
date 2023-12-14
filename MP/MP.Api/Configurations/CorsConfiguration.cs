namespace MP.Api.Configurations
{
    public static class CorsConfiguration
    {
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
    }
}