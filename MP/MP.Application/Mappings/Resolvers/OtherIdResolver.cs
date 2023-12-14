using AutoMapper;
using MP.Core.Interfaces.Services;

namespace MP.Application.Mappings.Resolvers
{
    public class OtherIdResolver<TSrc, TDest> : IMemberValueResolver<TSrc, TDest, string?, Guid?>
            where TSrc : class
            where TDest : class
    {
        protected readonly IExampleDomainService _domainService;

        public OtherIdResolver(IExampleDomainService domainService)
        {
            _domainService = domainService;
        }

        public Guid? Resolve(TSrc source, TDest destination, string? sourceMember, Guid? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return _domainService.GetByName(sourceMember.ToUpper()).Result?.Id;
            }
            return default;
        }
    }
}