using MP.CrossCutting.Utils.Interfaces.DomainService;
using MP.CrossCutting.Utils.Interfaces.Model;
using MP.CrossCutting.Utils.Interfaces.Repositories;
using System.Linq.Expressions;

namespace MP.CrossCutting.Utils.Model.DomainServices
{
    public abstract class DomainServiceBase<TEntity> : IDomainService<TEntity>
            where TEntity : class, IIdentifiable
    {
        protected readonly IRepository<TEntity> _repository;

        protected DomainServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task Create(TEntity entity) => await _repository.Insert(entity);

        public virtual async Task<TEntity?> Get(decimal id) => await _repository.GetById(id);

        public virtual async Task<TEntity?> Get(Expression<Func<TEntity, bool>> query) => await _repository.Get(query);

        public virtual async Task<IEnumerable<TEntity>> GetAll() => (await _repository.ListAll()).ToList();

        public virtual async Task<TEntity?> Remove(decimal id)
        {

            TEntity? entity = await Get(id);
            if (entity is not null)
            {
                await _repository.Delete(entity);
            }
            return entity;
        }

        public virtual async Task Update(TEntity entity) => await _repository.Update(entity);
    }
}