using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CME.Localization.Mongo
{
    public static class LocalizationStartupExtensions
    {
        public static void AddLocalizationSupport(this IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    "en",
                    "en-GB",
                    "en-US",
                    "ar"
                };
                options.SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            });
        }

        public static void UseLocalizationSupport(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
        }
    }
}
