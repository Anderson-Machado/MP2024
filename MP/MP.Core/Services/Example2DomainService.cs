using MP.Core.Entities;
using MP.Core.Entities.Complements;
using MP.Core.Entities.Dtos;
using MP.Core.Interfaces.Repositories;
using MP.Core.Interfaces.Services;
using MP.CrossCutting.Utils.Model.DomainServices;
using System.Linq.Expressions;

namespace MP.Core.Services
{
    public class Example2DomainService : DomainServiceBase<Example2>, IExample2DomainService
    {
        protected readonly IExample2Repository _example2Repository;

        public Example2DomainService(IExample2Repository repository)
            : base(repository)
        {
            _example2Repository = repository;
        }

        public override async Task<IEnumerable<Example2>> GetAll()
        {
            return (await _example2Repository.ListAll(EntityIncludeEntities.ExampleInclude())).ToList();
        }

        public override async Task<Example2?> Get(Expression<Func<Example2, bool>> query)
        {
            return await _example2Repository.Get(query, EntityIncludeEntities.ExampleInclude());
        }

        public override async Task<Example2?> Get(Guid id)
        {
            return await _example2Repository.GetById(id, EntityIncludeEntities.ExampleInclude());
        }

        public async Task<IEnumerable<Example2>> Search(SearchDto dto)
        {
            return await _example2Repository.SearchManyPaged((new Example2()).SearchQuery(dto),
                                                         dto.Page.GetValueOrDefault(1),
                                                         dto.PageSize.GetValueOrDefault(10),
                                                         EntityIncludeEntities.ExampleInclude());
        }

        public async Task<IEnumerable<Example2>>? ListByOtherName(string? otherName)
        {
            if (!string.IsNullOrWhiteSpace(otherName))
                return await _example2Repository.SearchMany(new Example2().Example2OtherNameQuery(otherName), EntityIncludeEntities.ExampleInclude());
            return Array.Empty<Example2>();
        }

        public async Task<int> SearchTotalCount(SearchDto dto)
        {
            return (int)await _example2Repository.Count((new Example2()).SearchQuery(dto), EntityIncludeEntities.ExampleInclude());
        }
    }
}