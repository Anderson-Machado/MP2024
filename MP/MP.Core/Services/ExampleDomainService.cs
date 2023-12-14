using MP.Core.Entities;
using MP.Core.Entities.Complements;
using MP.Core.Interfaces.Repositories;
using MP.Core.Interfaces.Services;
using MP.CrossCutting.Utils.Model.DomainServices;

namespace MP.Core.Services
{
    public class ExampleDomainService : DomainServiceBase<Example>, IExampleDomainService
    {

        protected readonly IExampleRepository _exampleRepository;

        public ExampleDomainService(IExampleRepository exampleRepository)
            : base(exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public async Task<Example?> GetByName(string? name)
        {
            return await _exampleRepository.Get(new Example().ExampleNameQuery(name));
        }
    }
}