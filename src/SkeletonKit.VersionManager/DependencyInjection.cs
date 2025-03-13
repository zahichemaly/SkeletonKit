using SkeletonKit.VersionManager.Abstractions.Repositories;
using SkeletonKit.VersionManager.Abstractions.Services;
using SkeletonKit.VersionManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SkeletonKit.VersionManager
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
