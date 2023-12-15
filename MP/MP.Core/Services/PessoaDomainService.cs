using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.Core.Interfaces.Services;

namespace MP.Core.Services
{
    public class PessoaDomainService : IPessoaDomainService
    {
        private readonly IPessoaRepositories _pessoaRepositories;

        public PessoaDomainService(IPessoaRepositories pessoaRepositories)
        {
            _pessoaRepositories = pessoaRepositories;
        }

        public async Task<Pessoa> GetPessoaByMatricula(decimal matricula)
        {
            return await _pessoaRepositories.GetPessoaByMatricula(matricula);
        }
    }
}
