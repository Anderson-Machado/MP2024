using Microsoft.EntityFrameworkCore;

namespace MP.CrossCutting.Utils.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable<TEntity> SetWithIncludes<TEntity>(this DbContext context, ICollection<string> navigationPropertyPaths)
            where TEntity : class
        {
            var query = context.Set<TEntity>().AsQueryable();

            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                query = query.Include(navigationPropertyPath);
            }

            return query;
        }

        public static IQueryable<TEntity> SetWithIncludesNoTracking<TEntity>(this DbContext context, ICollection<string> navigationPropertyPaths)
            where TEntity : class
        {
            var query = context.Set<TEntity>().AsNoTracking().AsQueryable();

            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                query = query.Include(navigationPropertyPath);
            }

            return query;
        }

        public static IQueryable<TEntity> SetNoTracking<TEntity>(this DbContext context)
            where TEntity : class
        {
            return context.Set<TEntity>().AsNoTracking().AsQueryable();
        }
    }
}