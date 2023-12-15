using MP.CrossCutting.Utils.Interfaces.Model;
using System.Linq.Expressions;

namespace MP.CrossCutting.Utils.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : IIdentifiable
    {
        Task Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task<TEntity?> GetById(decimal id, List<string>? navigationPropertyPaths = default);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = default);
        Task<IQueryable<TEntity>> ListAll(List<string>? navigationPropertyPaths = default);
    }
}