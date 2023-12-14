using AutoMapper;
using MP.Application.Models.Common;
using MP.Application.Models.Example;
using MP.Application.Services.Interfaces;
using MP.Core.Entities;
using MP.Core.Interfaces.Services;
using MP.CrossCutting.Utils.Resources;

namespace MP.Application.Services
{
    public class ExampleService : IExampleService
    {
        protected readonly IExampleDomainService _domainService;
        protected readonly IMapper _mapper;

        public ExampleService(IExampleDomainService domainService, IMapper mapper)
        {
            _domainService = domainService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ExampleModel>> Create(ExamplePostModel model)
        {
            var entity = _mapper.Map<Example>(model);

            await _domainService.Create(entity);

            if (!entity.IsValid)
            {
                return ServiceResult<ExampleModel>.CreateWithErrors(entity.Notifications);
            }

            return ServiceResult<ExampleModel>.CreateSuccess(_mapper.Map<ExampleModel>(entity));
        }

        public async Task<ServiceResult<ExampleModel>> Update(ExamplePatchModel model)
        {
            if (!model.ExampleId.HasValue || model.ExampleId.Value == Guid.Empty)
            {
                return ServiceResult<ExampleModel>.CreateWithError(nameof(model.ExampleId), Messages.InvalidID);
            }

            Example? dbEntity = await _domainService.Get(model.ExampleId.Value);
            if (dbEntity is null)
            {
                return ServiceResult<ExampleModel>.CreateNotFound();
            }

            _mapper.Map(model, dbEntity);

            await _domainService.Update(dbEntity);

            if (!dbEntity.IsValid)
            {
                return ServiceResult<ExampleModel>.CreateWithErrors(dbEntity.Notifications);
            }

            return ServiceResult<ExampleModel>.CreateSuccess(_mapper.Map<ExampleModel>(dbEntity));
        }

        public async Task<ServiceResult<ExampleModel>> Delete(Guid? id)
        {
            if (!id.HasValue || id == Guid.Empty)
            {
                return ServiceResult<ExampleModel>.CreateWithError("ExampleId", Messages.InvalidID);
            }

            Example? entity = await _domainService.Remove(id.Value);

            if (entity is null)
            {
                return ServiceResult<ExampleModel>.CreateNotFound();
            }

            return ServiceResult<ExampleModel>.CreateSuccess(_mapper.Map<ExampleModel>(entity));
        }

        public async Task<ServiceResult<ExampleModel>> Get(Guid? id)
        {
            if (!id.HasValue || id == Guid.Empty)
            {
                return ServiceResult<ExampleModel>.CreateWithError("ExampleId", Messages.InvalidID);
            }

            Example? dbEntity = await _domainService.Get(id.Value);

            if (dbEntity is null)
            {
                return ServiceResult<ExampleModel>.CreateNotFound();
            }

            return ServiceResult<ExampleModel>.CreateSuccess(_mapper.Map<ExampleModel>(dbEntity));
        }

        public async Task<ServiceResult<IEnumerable<ExampleModel>>> List()
        {
            return ServiceResult<IEnumerable<ExampleModel>>.CreateSuccess(_mapper.Map<IEnumerable<ExampleModel>>(await _domainService.GetAll()));
        }
    }
}