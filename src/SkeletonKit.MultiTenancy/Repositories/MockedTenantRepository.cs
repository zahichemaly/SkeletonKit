using CME.MultiTenancy.Abstractions.Repositories;
using CME.MultiTenancy.Entities;

namespace CME.MultiTenancy.Repositories
{
    /// <summary>
    /// Returns mocked tenants that are hardcoded for testing purposes.
    /// </summary>
    internal class MockedTenantRepository : ITenantRepository
    {
        private readonly IEnumerable<Tenant> _tenantList;

        public MockedTenantRepository()
        {
            _tenantList = new List<Tenant>()
            {
                new Tenant()
                {
                    Id = "1",
                    Name = "cme"
                }
            };
        }

        public Tenant Get(string id)
        {
            return _tenantList.FirstOrDefault(t => t.Name.ToLowerInvariant() == id);
        }

        public IEnumerable<Tenant> GetAll()
        {
            return _tenantList;
        }
    }
}
