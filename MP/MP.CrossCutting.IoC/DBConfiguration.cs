using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MP.Infrastructure.Data.Contexts;

namespace MP.CrossCutting.IoC
{
    public static class DBConfiguration
    {
        public static void AddBDConfiguration(this IServiceCollection services, IConfiguration cfg)
        {
            services.AddDbContext<ExampleDbContext>(options =>
            {
                options
                    .UseSqlServer(cfg.GetConnectionString("DbConnection"))
                    .UseExceptionProcessor();
            });
        }
    }
}