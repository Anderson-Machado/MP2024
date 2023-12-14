using Microsoft.EntityFrameworkCore;
using MP.Core.Entities;
using MP.Core.Interfaces.Repositories;
using MP.CrossCutting.Utils.Extensions;
using MP.CrossCutting.Utils.Model.Repositories;
using MP.Infrastructure.Data.Contexts;
using System.Linq.Expressions;

namespace MP.Infrastructure.Data.Repositories
{
    public class Example2Repository : RepositoryWithSearchBase<Example2>, IExample2Repository
    {
        public Example2Repository(ExampleDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Example2>> SearchManyPaged(Expression<Func<Example2, bool>> query,
                                                               int page, int pageSize,
                                                               List<string>? navigationPropertyPaths = null)
        {
            var skip = (page == 1) ? 0 : (page - 1) * pageSize;
            return await _dbContext.SetWithIncludesNoTracking<Example2>(navigationPropertyPaths!).Where(query)
                .OrderBy(f => f.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}