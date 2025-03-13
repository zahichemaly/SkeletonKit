using CME.MultiTenancy.Entities;

namespace CME.MultiTenancy.Configurations
{
    public sealed class TenantSettings
    {
        public IEnumerable<Tenant> Tenants { get; set; } = [];
    }
}
