using AutoMapper;
using MP.Application.Models.Common;
using MP.Application.Models.Pessoa;
using MP.Application.Services.Interfaces;
using MP.Core.Interfaces.Services;

namespace MP.Application.Services
{
    public class PessoaService : IPessoaService
    {
        protected readonly IMapper _mapper;
        private readonly IPessoaDomainService _domainService;
        public PessoaService(IMapper mapper, IPessoaDomainService domainService)
        {
            _mapper = mapper;
            _domainService = domainService;
        }

        public async Task<ServiceResult<PessoaModel>> GetPessoaByMatricula(decimal matricula)
        {

            var entity = await _domainService.GetPessoaByMatricula(matricula);

            if (entity == null)
            {
                return ServiceResult<PessoaModel>.CreateNotFound();
            }

            var dto = _mapper.Map<PessoaModel>(entity);

            return ServiceResult<PessoaModel>.CreateSuccess(dto);
        }
    }
}
