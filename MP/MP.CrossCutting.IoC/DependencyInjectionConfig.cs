using Microsoft.Extensions.DependencyInjection;
using MP.Application.Services;
using MP.Application.Services.Interfaces;
using MP.Core.Interfaces.Repositories;
using MP.Core.Interfaces.Services;
using MP.Core.Services;
using MP.Infrastructure.Data.Repositories;

namespace MP.CrossCutting.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApiConfigurationIoC(this IServiceCollection services)
        {
            services.AddScoped<IExampleRepository, ExampleRepository>();
            services.AddScoped<IExample2Repository, Example2Repository>();

            services.AddScoped<IExample2DomainService, Example2DomainService>();
            services.AddScoped<IExampleDomainService, ExampleDomainService>();

            services.AddScoped<IExample2Service, Example2Service>();
            services.AddScoped<IExampleService, ExampleService>();

            return services;
        }
    }
}