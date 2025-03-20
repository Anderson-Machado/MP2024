using MP.Core.Entities;
using MP.CrossCutting.Utils.Interfaces.Repositories;

namespace MP.Core.Interfaces.Repositories
{
    public interface IVisitanteRepository : IRepository<Visitante>, IRepositorySearch<Visitante>
    {
        Task<Visitante> GetVisitanteByMatricula(decimal codVisitante);
    }
}
