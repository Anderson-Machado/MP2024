using AutoMapper;
using MP.Application.Models.Common;
using MP.Core.Entities.Dtos;

namespace MP.Application.Mappings.Profiles
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            #region Example

            //CreateMap<ExamplePostModel, Example>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.Notifications, opt => opt.Ignore())
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .AfterMap((src, dest) => dest.Validate());


            //CreateMap<ExamplePatchModel, Example>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore())
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.Name) ? src.Name?.ToUpper() : dest.Name))
            //    .AfterMap((src, dest) => dest.Validate());

            #endregion

            #region Example2

            //CreateMap<Example2PostModel, Example2>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.ModifiedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
            //    .ForMember(dest => dest.Notifications, opt => opt.Ignore())
            //    .ForMember(dest => dest.OtherId, opt => opt.MapFrom<OtherIdResolver<Example2PostModel, Example2>, string?>(src => src.OtherName))
            //    .ForMember(dest => dest.Other, opt => opt.Ignore())
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            //    .AfterMap((src, dest) => dest.Validate());


            //CreateMap<Example2PatchModel, Example2>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore())
            //    .ForMember(dest => dest.Description, opt => opt.MapFrom((src, dest) => !string.IsNullOrEmpty(src.Description) ? src.Description?.ToUpper() : dest.Description))
            //    .ForMember(dest => dest.OtherId, opt =>
            //    {
            //        opt.PreCondition(src => !string.IsNullOrEmpty(src.OtherName));
            //        opt.MapFrom<OtherIdResolver<Example2PatchModel, Example2>, string?>(src => src.OtherName!);
            //    })
            //    .AfterMap((src, dest) => dest.Validate());

            #endregion

            #region Common

            CreateMap<SearchModel, SearchDto>()
                .BeforeMap((s, d, r) =>
                {
                    s.Validate();
                    d.AddNotifications(s.Notifications);
                })
                .ForMember(dest => dest.Term, opt => opt.MapFrom(src => src.Term))
                .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.Notifications, opt => opt.Ignore());

            #endregion

        }
    }
}