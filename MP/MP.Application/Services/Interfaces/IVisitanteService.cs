using MP.Application.Models.App;
using MP.Application.Models.Common;
using MP.Application.Models.Visita;

namespace MP.Application.Services.Interfaces
{
    public interface IVisitanteService
    {
        Task<ServiceResult<AppResponse>> GetVisitanteByMatricula(AppRequest app);
    }
}
