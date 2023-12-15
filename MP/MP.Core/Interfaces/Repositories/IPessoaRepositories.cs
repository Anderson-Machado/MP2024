using MP.Core.Entities;
using MP.CrossCutting.Utils.Interfaces.Repositories;

namespace MP.Core.Interfaces.Repositories
{
    public interface IPessoaRepositories : IRepository<Pessoa>, IRepositorySearch<Pessoa>
    {
        Task<Pessoa> GetPessoaByMatricula(decimal matricula);
    }
}



