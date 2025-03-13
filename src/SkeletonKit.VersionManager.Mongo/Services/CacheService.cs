using CacheManager.Core;
using SkeletonKit.MultiTenancy.Abstractions.Providers;
using SkeletonKit.VersionManager.Abstractions.Services;
using SkeletonKit.VersionManager.Models;

namespace SkeletonKit.VersionManager.Mongo.Services
{
    internal class CacheService : ICacheService
    {
        private readonly ICacheManager<AppVersion> _cache;
        private readonly ITenantProvider _tenantProvider;
        private const string DEFAULT_REGION = "AppVersion";

        public CacheService(ICacheManager<AppVersion> cache, ITenantProvider tenantProvider)
        {
            _cache = cache;
            _tenantProvider = tenantProvider;
        }

        private string GetRegion()
        {
            var tenantId = _tenantProvider.GetTenantId();
            if (string.IsNullOrWhiteSpace(tenantId)) return DEFAULT_REGION;
            return tenantId;
        }

        public async Task<AppVersion> GetAsync(string key, int expiration, Func<Task<AppVersion>> acquire, bool expirationInHours = true)
        {
            var region = GetRegion();
            var result = _cache.Get<AppVersion>(key, region);
            if (result == null)
            {
                result = await acquire();
                if (result != null)
                {
                    _cache.Add(key, result, region);
                }
            }
            return result;
        }

        public async Task RemoveAsync(string key)
        {
            var region = GetRegion();
            await Task.Run(() => Task.FromResult(_cache.Remove(key, region)));
        }
    }
}
