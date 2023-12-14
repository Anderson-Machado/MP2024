using MP.CrossCutting.Utils.Interfaces.Model;
using System.Linq.Expressions;

namespace MP.CrossCutting.Utils.Interfaces.Repositories
{
    public interface IRepositorySearchExtended<TEntity> where TEntity : IIdentifiable
    {
        Task<IEnumerable<TEntity>> SearchManyPaged(Expression<Func<TEntity, bool>> query,
                                                   int page, int pageSize,
                                                   List<string>? navigationPropertyPaths = default);
    }
}