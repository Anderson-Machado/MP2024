namespace MP.Api.Configurations.Swagger
{
    /// <summary>
    /// Classe para extração dos valores da seção "Swagger" do appsettings.json
    /// </summary>
    public class SwaggerSettings
    {
        public const string SETTINGS_KEY = "Swagger";
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string Version { get; set; } = "v1";
        public string? ContactName { get; set; }
        public string? ContactUrl { get; set; }
    }
}