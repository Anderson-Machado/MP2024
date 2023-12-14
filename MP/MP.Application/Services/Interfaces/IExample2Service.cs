using MP.Application.Models.Common;
using MP.Application.Models.Example2;

namespace MP.Application.Services.Interfaces
{
    public interface IExample2Service
    {
        Task<ServiceResult<Example2Model>> Create(Example2PostModel model);
        Task<ServiceResult<Example2Model>> Update(Example2PatchModel model);
        Task<ServiceResult<Example2Model>> Delete(Guid? id);
        Task<ServiceResult<Example2Model>> Get(Guid? id);
        Task<ServiceResult<IEnumerable<Example2Model>>> ListByOther(string? otherName);
        Task<ServiceResult<ListExample2Model>> SearchMany(SearchModel model);
    }
}