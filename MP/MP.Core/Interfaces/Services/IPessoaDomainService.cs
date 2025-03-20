using MP.Core.Entities;

namespace MP.Core.Interfaces.Services
{
    public interface IPessoaDomainService
    {
        Task<Pessoa> GetPessoaByMatricula(decimal matricula);
    }
}
