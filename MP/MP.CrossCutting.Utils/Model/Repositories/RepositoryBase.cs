using Microsoft.EntityFrameworkCore;
using MP.CrossCutting.Utils.Extensions;
using MP.CrossCutting.Utils.Interfaces.Model;
using MP.CrossCutting.Utils.Interfaces.Repositories;
using System.Linq.Expressions;

namespace MP.CrossCutting.Utils.Model.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IIdentifiable
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task Insert(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<TEntity?> GetById(decimal id, List<string>? navigationPropertyPaths = default)
        {
            if (navigationPropertyPaths is not null)
            {
                return await _dbContext.SetWithIncludes<TEntity>(navigationPropertyPaths).FirstOrDefaultAsync(e => e.Id == id);
            }

            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity?> Get(Expression<Func<TEntity, bool>> query, List<string>? navigationPropertyPaths = default)
        {
            if (navigationPropertyPaths is not null)
            {
                return await _dbContext.SetWithIncludes<TEntity>(navigationPropertyPaths).FirstOrDefaultAsync(query);
            }

            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(query);
        }

        public virtual async Task<IQueryable<TEntity>> ListAll(List<string>? navigationPropertyPaths = null)
        {
            if (navigationPropertyPaths is not null)
            {
                return await Task.FromResult(_dbContext.SetWithIncludesNoTracking<TEntity>(navigationPropertyPaths));
            }

            return await Task.FromResult(_dbContext.SetNoTracking<TEntity>());
        }
    }
}