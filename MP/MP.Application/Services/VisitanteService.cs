using AutoMapper;
using MP.Application.Models.App;
using MP.Application.Models.Common;
using MP.Application.Models.Visita;
using MP.Application.Services.Interfaces;
using MP.Core.Entities;
using MP.Core.Interfaces.Services;

namespace MP.Application.Services
{
    public class VisitanteService : IVisitanteService
    {
        private readonly IVisitanteDomainService _domainService;
        protected readonly IMapper _mapper;
        private readonly ILogAcessoDomainService _logAcessoDomainService;

        public VisitanteService(IVisitanteDomainService domainService, IMapper mapper, ILogAcessoDomainService logAcessoDomainService)
        {
            _domainService = domainService;
            _mapper = mapper;
            _logAcessoDomainService = logAcessoDomainService;
        }

        public async Task<ServiceResult<AppResponse>> GetVisitanteByMatricula(AppRequest app)
        {
            var dataAtual = DateTime.Now.ToString("dd-MM-yyyy HH:mm"); // rever de levar este carinha para o body

            var data = dataAtual.Remove(10).Replace("-", "");
            var hora = dataAtual.Substring(10).Replace(" ", "").Replace(":", "");
            var entity = await _domainService.GetVisitanteByMatricula(app.Matricula);
            var res = new AppResponse();
            if (entity is null)
            {
                return ServiceResult<AppResponse>.CreateWithError(app.Matricula.ToString(), "Credencial não de visitante não cadastrada.");
            }
            if(entity.Result == "RECUSADO!")
            {
                return ServiceResult<AppResponse>.CreateWithError(app.Matricula.ToString(), "RECUSADO!");
            }
            var entityLogAcesso = new LogAcesso()
            {
                Matricula = app.Matricula,
                Credencial = app.Matricula,
                Equipamento = app.Equipamento,
                DataRequisicao = DateTime.Parse(dataAtual),
                SendidoConsulta = app.Sentido,
                Evento = 9,
                CodAreaOrigem = app.AreaDe,
                CodAreaDestino = app.AreaPara,
                CodVisitante = decimal.Parse(entity.Matricula),
                Funcao = 0,
                CodGrupo = 3,
                DataPersistencia = DateTime.Parse(dataAtual),
                NuDataRequisicao = int.Parse(data),
                NuHoraRequisicao = int.Parse(hora),
                Nome = entity.Nome
            };

            entityLogAcesso.DefinirAreasComBaseNoSentidoConsulta();
            await _logAcessoDomainService.Create(entityLogAcesso);
            res.Message = entity.Result;

            return ServiceResult<AppResponse>.CreateSuccess(res);
        }
    }
}
