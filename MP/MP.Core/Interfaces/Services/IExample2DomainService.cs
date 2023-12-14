using MP.Core.Entities;
using MP.Core.Entities.Dtos;
using MP.CrossCutting.Utils.Interfaces.DomainService;

namespace MP.Core.Interfaces.Services
{
    public interface IExample2DomainService : IDomainService<Example2>
    {
        Task<IEnumerable<Example2>> Search(SearchDto dto);
        Task<int> SearchTotalCount(SearchDto dto);
        Task<IEnumerable<Example2>>? ListByOtherName(string? otherName);
    }
}