using Microsoft.EntityFrameworkCore;
using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.CrossCutting.Utils.Model.Repositories;
using MP.Infrastructure.Data.Contexts;

namespace MP.Infrastructure.Data.Repositories
{
    public class VisitanteRepository : RepositoryWithSearchBase<Visitante>, IVisitanteRepository
    {
        public VisitanteRepository(MPDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Visitante> GetVisitanteByMatricula(decimal codVisitante)
        {
            var result = await _dbContext.Set<Visitante>().Where(x => x.Id == codVisitante).FirstOrDefaultAsync();
            return result;
        }
    }
}
