using MP.Core.Entities;
using MP.CrossCutting.Utils.Interfaces.Repositories;

namespace MP.Core.Interfaces.Repositories
{
    public interface IExampleRepository : IRepository<Example>, IRepositorySearch<Example>
    {
    }
}