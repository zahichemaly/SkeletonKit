using CME.MultiTenancy.Abstractions.Repositories;
using CME.MultiTenancy.Configurations;
using CME.MultiTenancy.Entities;

namespace CME.MultiTenancy.Repositories
{
    /// <summary>
    /// Returns tenants that are defined in a configuration file such as in the AppSettings.json.
    /// </summary>
    public class LocalTenantRepository : ITenantRepository
    {
        private readonly Dictionary<string, Tenant> _inMemoryTenants;

        public LocalTenantRepository(TenantSettings tenantSettings)
        {
            var settings = tenantSettings;
            if (settings != null && settings.Tenants != null && settings.Tenants.Any())
            {
                _inMemoryTenants = settings.Tenants.ToDictionary(x => x.Name.ToLowerInvariant(), x => x);
            }
            else
            {
                _inMemoryTenants = [];
            }
        }

        public Tenant Get(string id)
        {
            if (_inMemoryTenants.TryGetValue(id, out var tenant)) { return tenant; }
            return null;
        }

        public IEnumerable<Tenant> GetAll()
        {
            return _inMemoryTenants.Values.ToList();
        }
    }
}
