using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.Core.Interfaces.Services;


namespace MP.Core.Services
{
    public class VisitanteDomainService : IVisitanteDomainService
    {
        private readonly IVisitaRepository _visitaRepository;
        private readonly IVisitanteRepository _visitanteRepository;

        public VisitanteDomainService(IVisitaRepository visitaRepository, IVisitanteRepository visitanteRepository)
        {
            _visitaRepository = visitaRepository;
            _visitanteRepository = visitanteRepository;
        }

        public async Task<Visitante> GetVisitanteByMatricula(decimal codVisitante)
        {
            var visita = await GetVisitaByMatricula(codVisitante);
            if(visita is null)
            {
               return null;
            }

            if(visita.DataBaixaCredencial is null)
            {
                var result = await _visitanteRepository.GetVisitanteByMatricula(visita.CodVisitante);
                result.Matricula = codVisitante.ToString();
                result.Result = "LIBERADO!";
                result.VitaNumero = visita.Id;
                result.VisiNumero = visita.CodVisitante;
                return result;
            }


            return new Visitante() { Result = "RECUSADO!"};
        }

        private async Task<Visita> GetVisitaByMatricula(decimal matricula)
        {
            var result = await _visitaRepository.GetVisitaByMatricula(matricula);
            return result;
        }
    }
}
