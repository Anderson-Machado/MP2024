using MP.CrossCutting.Utils.Extensions;
using StackExchange.Redis;
using System.Reflection;

namespace MP.Api.Configurations
{
    public static class CacheConfig
    {
        public static void AddDistributedCache(this IServiceCollection services, IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            if (environment.IsLocal())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.ConfigurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("CacheConnection"));
                    options.ConfigurationOptions.CertificateValidation += (_, _, _, _) => true; // Fixes RemoteCertificateNameMismatch error when running on AKS

                    options.InstanceName = Assembly.GetExecutingAssembly().GetName().Name + "/";
                });
            }
        }
    }
}