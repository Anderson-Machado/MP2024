using MP.Application.Models.App;
using MP.Application.Models.Common;
using MP.Application.Models.Pessoa;

namespace MP.Application.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<ServiceResult<AppResponse>> GetPessoaByMatricula(AppRequest app);

        Task<ServiceResult<AppResponse>> BatchOffLine(IEnumerable<AppRequest> app);
    }
}
