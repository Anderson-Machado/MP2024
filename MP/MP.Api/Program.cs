using Microsoft.AspNetCore.HttpOverrides;
using MP.Api.Configurations;
using MP.Api.Configurations.Authorization;
using MP.Api.Configurations.HealthCheck;
using MP.Api.Configurations.Swagger;
using MP.CrossCutting.IoC;
using MP.CrossCutting.ProblemDetail;
using MP.CrossCutting.Utils.Converts;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

Log.Information("Iniciando aplicação");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

    builder.Host.ConfigureSerilog();

    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new IsoDateConverter());
    });

    builder.Services.AddCorrelationIdConfiguration();

    builder.Services.AddHealthCheckConfiguration(builder.Configuration);

    builder.Services.AddCustomSwaggerConfiguration(builder.Environment);

    builder.Services.AddApiConfigurationIoC();

    builder.Services.AddAutoMapperConfiguration();

    builder.Services.AddProblemDetailsConfiguration();

    builder.Services.AddCultureLocalization();

    builder.Services.AddBDConfiguration(builder.Configuration);

    builder.Services.AddAuthConfiguration();

    //builder.Services.AddDistributedCache(builder.Configuration, builder.Environment);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseRequestLocalization();

    app.UseCustomCorrelationId();

    app.UseForwardedHeaders(new ForwardedHeadersOptions // Add IP correto do request
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    if (!app.Environment.IsProduction())
    {
        app.UseDeveloperExceptionPage();
        app.UseCustomSwaggerConfiguration();
    }

    app.UseCustomProblemDetails();

    app.UseRequestLogConfiguration();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.ConfigureCors();

    app.UseAuthConfiguration();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers().BypassAuthOnLocalEnvironment(app.Environment);
        endpoints.MapHealthCheckConfiguration();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação encerrou de forma inesperada");
}
finally
{
    Log.CloseAndFlush();
}

