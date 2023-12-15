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
            services.AddScoped<IPessoaRepositories, PessoaRepository>();

            services.AddScoped<IPessoaDomainService, PessoaDomainService>();

            services.AddScoped<IPessoaService, PessoaService>();

            return services;
        }
    }
}