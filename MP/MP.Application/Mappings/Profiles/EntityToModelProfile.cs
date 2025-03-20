using AutoMapper;
using MP.Application.Mappings.Resolvers;
using MP.Application.Models.Pessoa;
using MP.Application.Models.Visita;
using MP.Core.Entities;

namespace MP.Application.Mappings.Profiles
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            #region Pessoa

            CreateMap<Pessoa, PessoaModel>()
                .ForMember(dest => dest.HasValidAccess, opt => opt.MapFrom<HasValidAccessResolver>());

            #endregion

            CreateMap<Visitante, VisitanteModel>()
                .ForMember(dest => dest.Result, opt => opt.MapFrom(x=> string.IsNullOrEmpty(x.Result)? "RECUSADO": x.Result));


        }
    }
}