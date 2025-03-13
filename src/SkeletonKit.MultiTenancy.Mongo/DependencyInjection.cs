using CacheManager.Core;
using SkeletonKit.MultiTenancy.Abstractions.Repositories;
using SkeletonKit.MultiTenancy.Entities;
using SkeletonKit.MultiTenancy.Mongo.Repositories;
using SkeletonKit.MultiTenancy.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SkeletonKit.MultiTenancy.Mongo
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Reads tenant definitions from MongoDB and cache them using <seealso cref="CacheManager"/>.
        /// </summary>
        public static IServiceCollection AddCachedMongo(this IServiceCollection services)
        {
            services.AddSingleton<LocalTenantRepository>();
            services.AddSingleton<MongoTenantRepository>();
            services.AddSingleton<ITenantRepository>(provider =>
            {
                var mongoRepository = provider.GetRequiredService<MongoTenantRepository>();
                var cache = provider.GetRequiredService<ICacheManager<Tenant>>();
                return new CachedTenantRepository(mongoRepository, cache);
            });
            return services;
        }
    }
}
