using AutoMapper;
using MP.Application.Models.App;
using MP.Application.Models.Common;
using MP.Application.Models.Pessoa;
using MP.Application.Services.Interfaces;
using MP.Core.Entities;
using MP.Core.Interfaces.Services;

namespace MP.Application.Services
{
    public class PessoaService : IPessoaService
    {
        protected readonly IMapper _mapper;
        private readonly IPessoaDomainService _domainService;
        private readonly ILogAcessoDomainService _logAcessoDomainService;

        public PessoaService(IMapper mapper, IPessoaDomainService domainService, ILogAcessoDomainService logAcessoDomainService)
        {
            _mapper = mapper;
            _domainService = domainService;
            _logAcessoDomainService = logAcessoDomainService;
        }

        public async Task<ServiceResult<AppResponse>> GetPessoaByMatricula(AppRequest app)
        {
            var dataAtualDT = DateTime.Now;
            var dataAtual = dataAtualDT.ToString("dd-MM-yyyy HH:mm"); // rever de levar este carinha para o body

            var data = dataAtual.Remove(10).Replace("-", "");
            var hora = dataAtual.Substring(10).Replace(" ", "").Replace(":","");
            var res = new AppResponse();

            var entity = await _domainService.GetPessoaByMatricula(app.Matricula);
            var dto = _mapper.Map<PessoaModel>(entity);

            //Não encontrou usuário
            if (entity == null)
            {
               
                var entityLogAcesso = new LogAcesso()
                {
                    Matricula = app.Matricula,
                    Credencial = app.Matricula,
                    Equipamento = app.Equipamento,
                    DataRequisicao = dataAtualDT,
                    SendidoConsulta = app.Sentido,
                    Evento = 9,
                    CodAreaOrigem = app.AreaDe,
                    CodAreaDestino = app.AreaPara,
                    CodVisitante = 0,
                    Funcao = 0,
                    CodGrupo = 3,
                    DataPersistencia = dataAtualDT,
                    NuDataRequisicao = int.Parse(data),
                    NuHoraRequisicao = int.Parse(hora),
                    Nome = ""
                };
                entityLogAcesso.DefinirAreasComBaseNoSentidoConsulta();
                await _logAcessoDomainService.Create(entityLogAcesso);
                return ServiceResult<AppResponse>.CreateWithError(app.Matricula.ToString(), "Cred não cadastrado!");
            }
            else
            {
                if(entity.CodSituacaoPessoa==8 || entity.CodSituacaoPessoa == 17)
                {
                    var entityLogAcesso = new LogAcesso()
                    {
                        Matricula = app.Matricula,
                        Credencial = app.Matricula,
                        Equipamento = app.Equipamento,
                        DataRequisicao = dataAtualDT,
                        SendidoConsulta = app.Sentido,
                        Evento = 10,
                        CodAreaOrigem = app.AreaDe,
                        CodAreaDestino = app.AreaPara,
                        CodVisitante = 0,
                        Funcao = 0,
                        CodGrupo = 3,
                        DataPersistencia = dataAtualDT,
                        NuDataRequisicao = int.Parse(data),
                        NuHoraRequisicao = int.Parse(hora),
                        Nome = entity.NomePessoa
                    };
                    entityLogAcesso.DefinirAreasComBaseNoSentidoConsulta();
                    await _logAcessoDomainService.Create(entityLogAcesso);
                    res.Imagem = entity.Imagem;
                    res.Message = "Liberado";
                    res.Name = entity.NomePessoa;
                    return ServiceResult<AppResponse>.CreateSuccess(res);

                }
               else if (entity.CodSituacaoPessoa == 18 && entity.CodSituacaoPessoa == 23)
                {
                    var entityLogAcesso = new LogAcesso()
                    {
                        Matricula = app.Matricula,
                        Credencial = app.Matricula,
                        Equipamento = app.Equipamento,
                        DataRequisicao = dataAtualDT,
                        SendidoConsulta = app.Sentido,
                        Evento = 0,
                        CodAreaOrigem = app.AreaDe,
                        CodAreaDestino = app.AreaPara,
                        CodVisitante = 0,
                        Funcao = 0,
                        CodGrupo = 3,
                        DataPersistencia = dataAtualDT,
                        NuDataRequisicao = int.Parse(data),
                        NuHoraRequisicao = int.Parse(hora),
                        Nome = entity.NomePessoa,

                    };
                    if (!dto.HasValidAccess)
                    {
                        entityLogAcesso.Evento = 6;
                        entityLogAcesso.DefinirAreasComBaseNoSentidoConsulta();
                        await _logAcessoDomainService.Create(entityLogAcesso);
                        res.Name = entity.NomePessoa;
                        res.Message = "Funcionário bloqueado por validade!!";
                        return ServiceResult<AppResponse>.CreateSuccess(res);
                        
                    }
                    else
                    {
                        entityLogAcesso.Evento = 10;
                        entityLogAcesso.DefinirAreasComBaseNoSentidoConsulta();
                        await _logAcessoDomainService.Create(entityLogAcesso);
                        res.Imagem = entity.Imagem;
                        res.Name = entity.NomePessoa;
                        res.Message = "Liberado!";
                      return  ServiceResult<AppResponse>.CreateSuccess(res);
                    }
                }
                else if (entity.CodSituacaoPessoa !=8 || entity.CodSituacaoPessoa != 17 || entity.CodSituacaoPessoa!=18 || entity.CodSituacaoPessoa != 23)
                {
                    var entityLogAcesso = new LogAcesso()
                    {
                        Matricula = app.Matricula,
                        Credencial = app.Matricula,
                        Equipamento = app.Equipamento,
                        DataRequisicao = dataAtualDT,
                        SendidoConsulta = app.Sentido,
                        Evento = 10,
                        CodAreaOrigem = app.AreaDe,
                        CodAreaDestino = app.AreaPara,
                        CodVisitante = 0,
                        Funcao = 0,
                        CodGrupo = 3,
                        DataPersistencia = dataAtualDT,
                        NuDataRequisicao = int.Parse(data),
                        NuHoraRequisicao = int.Parse(hora),
                        Nome = entity.NomePessoa,
                    };
                    entityLogAcesso.DefinirAreasComBaseNoSentidoConsulta();
                    await _logAcessoDomainService.Create(entityLogAcesso);
                    res.Name = entity.NomePessoa;
                    res.Message = "Funcionário Bloqueado!";
                    return ServiceResult<AppResponse>.CreateSuccess(res);
                }
            }
            //rever o retorno de sucesso
            return ServiceResult<AppResponse>.CreateUnprocessable();
        }
    }
}
