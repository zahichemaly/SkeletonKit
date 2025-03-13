using SkeletonKit.VersionManager.Mongo.Repositories;
using SkeletonKit.VersionManager.Mongo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SkeletonKit.VersionManager.Mongo
{
    public static class DependencyInjection
    {
        public static void AddMongoVersionManager(this IServiceCollection services)
        {
            services.AddVersionManager<CacheService, MongoAppVersionRepository>();
        }
    }
}
