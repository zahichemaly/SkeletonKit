using SkeletonKit.MultiTenancy.Entities;

namespace SkeletonKit.MultiTenancy.Abstractions.Providers
{
    public interface ITenantProvider
    {
        string GetTenantId();
        Tenant GetTenant();
    }
}
