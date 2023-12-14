using Microsoft.EntityFrameworkCore;
using MP.CrossCutting.Utils.Extensions;
using MP.CrossCutting.Utils.Interfaces.Model;
using MP.CrossCutting.Utils.Interfaces.Repositories;
using System.Linq.Expressions;

namespace MP.CrossCutting.Utils.Model.Repositories
{
    public abstract class RepositoryWithSearchBase<TEntity> : RepositoryBase<TEntity>, IRepositorySearch<TEntity> where TEntity : class, IIdentifiable
    {
        protected RepositoryWithSearchBase(DbContext dbContext)
            : base(dbContext)
        {
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = null)
        {
            if (navigationPropertyPaths is not null)
            {
                return await _dbContext.SetWithIncludesNoTracking<TEntity>(navigationPropertyPaths).LongCountAsync(query);
            }

            return await _dbContext.Set<TEntity>().LongCountAsync(query);
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> query) => await _dbContext.Set<TEntity>().AnyAsync(query);

        public virtual async Task<TEntity?> Search(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = null)
        {
            if (navigationPropertyPaths is not null)
            {
                return await _dbContext.SetWithIncludesNoTracking<TEntity>(navigationPropertyPaths).FirstOrDefaultAsync(query);
            }
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(query);
        }

        public virtual async Task<IQueryable<TEntity>> SearchMany(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = null)
        {
            if (navigationPropertyPaths is not null)
            {
                return await Task.FromResult(_dbContext.SetWithIncludesNoTracking<TEntity>(navigationPropertyPaths).Where(query));
            }

            return await Task.FromResult(_dbContext.SetNoTracking<TEntity>().Where(query));
        }
    }
}