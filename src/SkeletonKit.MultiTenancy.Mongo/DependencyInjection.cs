using CacheManager.Core;
using CME.MultiTenancy.Abstractions.Repositories;
using CME.MultiTenancy.Entities;
using CME.MultiTenancy.Mongo.Repositories;
using CME.MultiTenancy.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CME.MultiTenancy.Mongo
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
