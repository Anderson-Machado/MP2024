using Serilog;
using System.Reflection;

namespace MP.Api.Configurations
{
    public static class SerilogConfig
    {
        /// <summary>
        /// Método que extende IHostBuilder encapsulando a configuração customizada  do serilog
        /// Deve ser chamado logo após a criação do builder na propriedade Host
        /// builder.Host.ConfigureSerilog()
        /// </summary>
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder)
        {
            Assembly assembly = typeof(Program).Assembly;
            string appName = assembly.GetName().Name!;
            string appVersion = assembly.GetName().Version!.ToString();

            builder.UseSerilog((hostBuilderContext, services, loggerConfiguration) => loggerConfiguration
                .WriteTo.Console()
                .ReadFrom.Configuration(hostBuilderContext.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", hostBuilderContext.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("ApplicationName", appName)
                .Enrich.WithProperty("Version", appVersion)
            );

            return builder;
        }
    }
}