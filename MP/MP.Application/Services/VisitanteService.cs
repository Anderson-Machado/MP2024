using AutoMapper;
using MP.Application.Models.Common;
using MP.Application.Models.Visita;
using MP.Application.Services.Interfaces;
using MP.Core.Interfaces.Services;

namespace MP.Application.Services
{
    public class VisitanteService : IVisitanteService
    {
        private readonly IVisitanteDomainService _domainService;
        protected readonly IMapper _mapper;

        public VisitanteService(IVisitanteDomainService domainService, IMapper mapper)
        {
            _domainService = domainService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<VisitanteModel>> GetVisitanteByMatricula(decimal codVisitante)
        {
            var entity = await _domainService.GetVisitanteByMatricula(codVisitante);

            var ret = _mapper.Map<VisitanteModel>(entity);

            return ServiceResult<VisitanteModel>.CreateSuccess(ret);

        }
    }
}
