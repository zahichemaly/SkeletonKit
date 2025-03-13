using CME.MultiTenancy.Entities;

namespace CME.MultiTenancy.Abstractions.Providers
{
    public interface ITenantProvider
    {
        string GetTenantId();
        Tenant GetTenant();
    }
}
