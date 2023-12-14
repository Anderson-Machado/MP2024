using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.CrossCutting.Utils.Model.Repositories;
using MP.Infrastructure.Data.Contexts;

namespace MP.Infrastructure.Data.Repositories
{
    public class ExampleRepository : RepositoryWithSearchBase<Example>, IExampleRepository
    {
        public ExampleRepository(ExampleDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}