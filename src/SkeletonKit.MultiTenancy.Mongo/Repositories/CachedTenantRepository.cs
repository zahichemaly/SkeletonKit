using CacheManager.Core;
using SkeletonKit.MultiTenancy.Abstractions.Repositories;
using SkeletonKit.MultiTenancy.Entities;

namespace SkeletonKit.MultiTenancy.Mongo.Repositories
{
    internal class CachedTenantRepository : ITenantRepository
    {
        private readonly MongoTenantRepository _tenantRepository;
        private readonly ICacheManager<Tenant> _cache;

        public CachedTenantRepository(MongoTenantRepository tenantRepository, ICacheManager<Tenant> cache)
        {
            _tenantRepository = tenantRepository;
            _cache = cache;
        }

        public Tenant Get(string id)
        {
            var result = _cache.Get<Tenant>(id, Constants.CACHE_REGION);
            if (result == null)
            {
                var latest = _tenantRepository.Get(id);
                if (latest != null)
                {
                    _cache.Add(id, latest, Constants.CACHE_REGION);
                }
                return latest;
            }
            return result;
        }

        public IEnumerable<Tenant> GetAll()
        {
            return _tenantRepository.GetAll();
        }
    }
}
