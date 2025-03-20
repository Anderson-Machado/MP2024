using Microsoft.Extensions.DependencyInjection;
using MP.Application.Mappings.Profiles;
using MP.Application.Mappings.Resolvers;

namespace MP.CrossCutting.IoC
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(cfg => cfg.ShouldUseConstructor = ci => !ci.IsPrivate,
                                          typeof(EntityToModelProfile),
                                          typeof(ModelToEntityProfile),
                                          typeof(HasValidAccessResolver));

            return services;
        }
    }
}