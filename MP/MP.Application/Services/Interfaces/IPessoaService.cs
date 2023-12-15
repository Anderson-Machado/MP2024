using MP.Application.Models.Common;
using MP.Application.Models.Pessoa;

namespace MP.Application.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<ServiceResult<PessoaModel>> GetPessoaByMatricula(decimal matricula);
    }
}
