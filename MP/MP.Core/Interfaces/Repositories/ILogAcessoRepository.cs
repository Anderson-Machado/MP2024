using MP.Core.Entities;
using MP.CrossCutting.Utils.Interfaces.Repositories;
using MP.CrossCutting.Utils.Model;


namespace MP.Core.Interfaces.Repositories
{
    public interface ILogAcessoRepository: IRepository<LogAcesso>, IRepositorySearch<LogAcesso>
    {
        Task Create(LogAcesso entity);
    }
}
