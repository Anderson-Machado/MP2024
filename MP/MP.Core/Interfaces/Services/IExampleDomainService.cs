using MP.Core.Entities;
using MP.CrossCutting.Utils.Interfaces.DomainService;

namespace MP.Core.Interfaces.Services
{
    public interface IExampleDomainService : IDomainService<Example>
    {
        Task<Example?> GetByName(string? name);
    }
}