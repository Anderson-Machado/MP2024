﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MP.Core.Entities;
using MP.CrossCutting.Utils.Extensions;
using System.Reflection;

namespace MP.Infrastructure.Data.Contexts
{
    public class MPDbContext : DbContext
    {
        private readonly IHostEnvironment _environment;
        private readonly ILogger<MPDbContext> _logger;

        public MPDbContext(DbContextOptions<MPDbContext> options,
                                         IHostEnvironment environment,
                                         ILogger<MPDbContext> logger)
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

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<SituacaoPessoaMultipla> SituacaoPessoa { get; set; }

    }
}