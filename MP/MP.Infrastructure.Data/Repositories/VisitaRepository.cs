using Microsoft.EntityFrameworkCore;
using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.CrossCutting.Utils.Model.Repositories;
using MP.Infrastructure.Data.Contexts;

namespace MP.Infrastructure.Data.Repositories
{
    public class VisitaRepository : RepositoryWithSearchBase<Visita>, IVisitaRepository
    {
        public VisitaRepository(MPDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Visita> GetVisitaByMatricula(decimal matricula)
        {
            var result = await _dbContext.Set<Visita>().Where(x => x.Observacao == matricula.ToString()).OrderByDescending(x=>x.Id).FirstOrDefaultAsync();

            return result;
        }
    }
}
