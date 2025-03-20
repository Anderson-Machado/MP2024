using MP.Core.Entities;
using MP.CrossCutting.Utils.Interfaces.Repositories;

namespace MP.Core.Interfaces.Repositories
{

    public interface IVisitaRepository: IRepository<Visita>, IRepositorySearch<Visita>
    {
        /// <summary>
        /// Consulta na tabela visita se DS_OBSERVACAO é igual ao numero de matricula
        /// </summary>
        /// <param name="matricula"></param>
        /// <returns></returns>
        Task<Visita> GetVisitaByMatricula(decimal matricula);
    }
}
