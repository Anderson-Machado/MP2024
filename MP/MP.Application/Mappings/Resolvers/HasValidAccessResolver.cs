using AutoMapper;
using MP.Application.Models.Pessoa;
using MP.Core.Entities;

namespace MP.Application.Mappings.Resolvers
{
    public class HasValidAccessResolver : IValueResolver<Pessoa, PessoaModel, bool>
    {
        public bool Resolve(Pessoa source, PessoaModel destination, bool destMember, ResolutionContext context)
        {
            if (source.CodSituacaoPessoa == 18 || source.CodSituacaoPessoa == 23)
            {
                var validate = source.SituacaoPessoa.DatePeriodoFinal is null ? DateTime.MaxValue : source.SituacaoPessoa.DatePeriodoFinal;
                return DateTime.Now > validate ? false : true;
            }
            else
            {
                return destination.HasValidAccess;
            }
        }
    }
}
