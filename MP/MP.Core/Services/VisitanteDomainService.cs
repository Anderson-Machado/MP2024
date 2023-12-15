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
            var teste = await GetVisitaByMatricula(codVisitante);
            if(teste is null)
            {
               return new Visitante();
            }

            if(teste.DataBaixaCredencial is null)
            {
                var result = await _visitanteRepository.GetVisitanteByMatricula(teste.CodVisitante);
                result.Matricula = codVisitante.ToString();
                result.Result = "LIBERADO";
                result.VitaNumero = teste.Id;
                result.VisiNumero = teste.CodVisitante;
                return result;
            }


            return new Visitante();
        }

        private async Task<Visita> GetVisitaByMatricula(decimal matricula)
        {
            var result = await _visitaRepository.GetVisitaByMatricula(matricula);
            return result;
        }
    }
}
