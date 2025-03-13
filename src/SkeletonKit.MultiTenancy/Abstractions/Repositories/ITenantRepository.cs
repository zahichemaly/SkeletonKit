using CME.MultiTenancy.Entities;

namespace CME.MultiTenancy.Abstractions.Repositories
{
    public interface ITenantRepository
    {
        Tenant Get(string id);
        IEnumerable<Tenant> GetAll();
    }
}
