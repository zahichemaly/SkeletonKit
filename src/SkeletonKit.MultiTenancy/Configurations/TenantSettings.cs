using SkeletonKit.MultiTenancy.Entities;

namespace SkeletonKit.MultiTenancy.Configurations
{
    public sealed class TenantSettings
    {
        public IEnumerable<Tenant> Tenants { get; set; } = [];
    }
}
