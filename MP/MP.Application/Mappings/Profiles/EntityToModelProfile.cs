using AutoMapper;
using MP.Application.Models.Example;
using MP.Application.Models.Example2;
using MP.Core.Entities;

namespace MP.Application.Mappings.Profiles
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            #region Example

            CreateMap<Example, ExampleModel>()
                .ForMember(dest => dest.ExampleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            #endregion

            #region Example2

            CreateMap<Example2, Example2Model>()
                .ForMember(dest => dest.Example2Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OtherName, opt => opt.MapFrom((src, dest) => src.Other?.Name));

            #endregion
        }
    }
}