using MP.CrossCutting.Utils.Interfaces.Model;
using System.Linq.Expressions;

namespace MP.CrossCutting.Utils.Interfaces.DomainService
{
    public interface IDomainService<TEntity> where TEntity : IIdentifiable
    {
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task<TEntity?> Remove(decimal id);
        Task<TEntity?> Get(decimal id);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> query);
        Task<IEnumerable<TEntity>> GetAll();
    }
}