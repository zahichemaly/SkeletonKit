using CME.VersionManager.Mongo.Repositories;
using CME.VersionManager.Mongo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CME.VersionManager.Mongo
{
    public static class DependencyInjection
    {
        public static void AddMongoVersionManager(this IServiceCollection services)
        {
            services.AddVersionManager<CacheService, MongoAppVersionRepository>();
        }
    }
}
