using AutoMapper;
using MP.Application.Models.Common;
using MP.Application.Models.Example2;
using MP.Application.Services.Interfaces;
using MP.Core.Entities;
using MP.Core.Entities.Dtos;
using MP.Core.Interfaces.Services;
using MP.CrossCutting.Utils.Resources;

namespace MP.Application.Services
{
    public class Example2Service : IExample2Service
    {
        protected readonly IExample2DomainService _domainService;
        protected readonly IMapper _mapper;

        public Example2Service(IExample2DomainService domainService, IMapper mapper)
        {
            _domainService = domainService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<Example2Model>> Create(Example2PostModel model)
        {
            var entity = _mapper.Map<Example2>(model);

            //await _domainService.Create(entity);

            if (!entity.IsValid)
            {
                return ServiceResult<Example2Model>.CreateWithErrors(entity.Notifications);
            }

            return ServiceResult<Example2Model>.CreateSuccess(_mapper.Map<Example2Model>(entity));
        }

        public async Task<ServiceResult<Example2Model>> Update(Example2PatchModel model)
        {
            if (!model.Example2Id.HasValue || model.Example2Id.Value == Guid.Empty)
            {
                return ServiceResult<Example2Model>.CreateWithError(nameof(model.Example2Id), Messages.InvalidID);
            }

            Example2? dbEntity = await _domainService.Get(model.Example2Id.Value);
            if (dbEntity is null)
            {
                return ServiceResult<Example2Model>.CreateNotFound();
            }

            _mapper.Map(model, dbEntity);

            await _domainService.Update(dbEntity);

            if (!dbEntity.IsValid)
            {
                return ServiceResult<Example2Model>.CreateWithErrors(dbEntity.Notifications);
            }

            return ServiceResult<Example2Model>.CreateSuccess(_mapper.Map<Example2Model>(dbEntity));
        }

        public async Task<ServiceResult<Example2Model>> Delete(Guid? id)
        {
            if (!id.HasValue || id == Guid.Empty)
            {
                return ServiceResult<Example2Model>.CreateWithError("Example2Id", Messages.InvalidID);
            }

            Example2? dbEntity = await _domainService.Get(id.Value);

            if (dbEntity is null)
            {
                return ServiceResult<Example2Model>.CreateNotFound();
            }

            /// logical deletion
            dbEntity.Delete();
            await _domainService.Update(dbEntity);

            if (!dbEntity.IsValid)
            {
                return ServiceResult<Example2Model>.CreateWithErrors(dbEntity.Notifications);
            }

            return ServiceResult<Example2Model>.CreateSuccess(_mapper.Map<Example2Model>(dbEntity));
        }

        public async Task<ServiceResult<Example2Model>> Get(Guid? id)
        {
            if (!id.HasValue || id == Guid.Empty)
            {
                return ServiceResult<Example2Model>.CreateWithError("Example2Id", Messages.InvalidID);
            }

            Example2? dbEntity = await _domainService.Get(id.Value);

            if (dbEntity is null)
            {
                return ServiceResult<Example2Model>.CreateNotFound();
            }

            return ServiceResult<Example2Model>.CreateSuccess(_mapper.Map<Example2Model>(dbEntity));
        }

        public async Task<ServiceResult<IEnumerable<Example2Model>>> ListByOther(string? otherName)
        {
            if (string.IsNullOrEmpty(otherName)) return ServiceResult<IEnumerable<Example2Model>>.CreateSuccess(Array.Empty<Example2Model>());

            return ServiceResult<IEnumerable<Example2Model>>.CreateSuccess(_mapper.Map<IEnumerable<Example2Model>>(await _domainService.ListByOtherName(otherName)!));
        }

        public async Task<ServiceResult<ListExample2Model>> SearchMany(SearchModel model)
        {
            #region Fail fast validation
            model.Validate();

            if (!model.IsValid)
            {
                return ServiceResult<ListExample2Model>.CreateWithErrors(model.Notifications);
            }

            #endregion

            var queryModel = _mapper.Map<SearchDto>(model);
            var totalItens = await _domainService.SearchTotalCount(queryModel);

            if (totalItens == 0)
            {
                return ServiceResult<ListExample2Model>.CreateSuccess(new ListExample2Model(Array.Empty<Example2Model>(), totalItens, 1, queryModel.PageSize.GetValueOrDefault(10)));
            }

            var dbEntities = await _domainService.Search(queryModel);
            var modelEntities = _mapper.Map<IEnumerable<Example2Model>>(dbEntities);

            return ServiceResult<ListExample2Model>.CreateSuccess(new ListExample2Model(modelEntities, totalItens, queryModel.Page.GetValueOrDefault(1), queryModel.PageSize.GetValueOrDefault(10)));
        }
    }
}