using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MP.Api.Configurations
{
    public static class LocalizationConfig
    {
        public static IServiceCollection AddCultureLocalization(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new[] { CultureInfo.GetCultureInfo("en"), CultureInfo.GetCultureInfo("pt") };
                options.DefaultRequestCulture = new RequestCulture(cultures[0]);
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            return services;
        }
    }
}