using MP.CrossCutting.Utils.Interfaces.Model;
using System.Linq.Expressions;

namespace MP.CrossCutting.Utils.Interfaces.Repositories
{
    public interface IRepositorySearch<TEntity> where TEntity : IIdentifiable
    {
        Task<TEntity?> Search(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = default);
        Task<IQueryable<TEntity>> SearchMany(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = default);
        Task<long> Count(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = default);
        Task<bool> Exists(Expression<Func<TEntity, bool>> query);
    }
}