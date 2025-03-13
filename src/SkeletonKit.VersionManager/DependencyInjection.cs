using CME.VersionManager.Abstractions.Repositories;
using CME.VersionManager.Abstractions.Services;
using CME.VersionManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CME.VersionManager
{
    public static class DependencyInjection
    {
        public static void AddVersionManager<TCacheService, TAppVersionRepository>(this IServiceCollection services) where TCacheService : class, ICacheService where TAppVersionRepository : class, IAppVersionRepository
        {
            services.AddTransient<ICacheService, TCacheService>();
            services.AddTransient<IAppVersionRepository, TAppVersionRepository>();
            services.AddTransient<IAppVersionService, AppVersionService>();
        }

        public static void UseVersionManager(this IApplicationBuilder app)
        {
            app.UseMiddleware<VersionManagerMiddleware>();
        }
    }
}
