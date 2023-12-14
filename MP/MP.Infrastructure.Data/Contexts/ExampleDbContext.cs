using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MP.Core.Entities;
using MP.CrossCutting.Utils.Extensions;
using System.Reflection;

namespace MP.Infrastructure.Data.Contexts
{
    public class ExampleDbContext : DbContext
    {
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExampleDbContext> _logger;

        public ExampleDbContext(DbContextOptions<ExampleDbContext> options,
                                         IHostEnvironment environment,
                                         ILogger<ExampleDbContext> logger)
           : base(options)
        {
            _environment = environment;
            _logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder = optionsBuilder
                .LogTo(msg => _logger.LogDebug(msg), new[] { DbLoggerCategory.Database.Command.Name, DbLoggerCategory.Query.Name });

            if (_environment.IsLocal() || _environment.IsDevelopment())
            {
                optionsBuilder = optionsBuilder
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplica todos os mappings contidos neste projeto
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Example> Examples { get; set; } = null!;
        public DbSet<Example2> Others { get; set; } = null!;
    }
}