using Microsoft.EntityFrameworkCore;
using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.CrossCutting.Utils.Model;
using MP.CrossCutting.Utils.Model.Repositories;
using MP.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Infrastructure.Data.Repositories
{
    public class LogAcessoRepository : RepositoryWithSearchBase<LogAcesso>, ILogAcessoRepository
    {
        public LogAcessoRepository(MPDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Create(LogAcesso entity)
        {
           await _dbContext.Set<LogAcesso>().AddAsync(entity);
            _dbContext.SaveChanges();
        }
    }
}
