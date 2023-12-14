using MP.Application.Models.Common;
using MP.Application.Models.Example;

namespace MP.Application.Services.Interfaces
{
    public interface IExampleService
    {
        Task<ServiceResult<ExampleModel>> Create(ExamplePostModel model);
        Task<ServiceResult<ExampleModel>> Update(ExamplePatchModel model);
        Task<ServiceResult<ExampleModel>> Delete(Guid? id);
        Task<ServiceResult<ExampleModel>> Get(Guid? id);
        Task<ServiceResult<IEnumerable<ExampleModel>>> List();
    }
}