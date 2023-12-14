using MP.Core.Entities;
using MP.CrossCutting.Utils.Interfaces.Repositories;

namespace MP.Core.Interfaces.Repositories
{
    public interface IExample2Repository : IRepository<Example2>, IRepositorySearch<Example2>, IRepositorySearchExtended<Example2>
    {
    }
}