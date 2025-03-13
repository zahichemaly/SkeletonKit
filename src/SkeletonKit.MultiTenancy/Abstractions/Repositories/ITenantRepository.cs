using SkeletonKit.MultiTenancy.Entities;

namespace SkeletonKit.MultiTenancy.Abstractions.Repositories
{
    public interface ITenantRepository
    {
        Tenant Get(string id);
        IEnumerable<Tenant> GetAll();
    }
}
