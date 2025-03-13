using CacheManager.Core;
using CME.MultiTenancy.Abstractions.Repositories;
using CME.MultiTenancy.Entities;

namespace CME.MultiTenancy.Mongo.Repositories
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
