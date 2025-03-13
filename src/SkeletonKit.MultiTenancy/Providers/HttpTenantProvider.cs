using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SkeletonKit.MultiTenancy.Abstractions.Providers;
using SkeletonKit.MultiTenancy.Abstractions.Repositories;
using SkeletonKit.MultiTenancy.Entities;
using System.Text;

namespace SkeletonKit.MultiTenancy.Services
{
    public class HttpTenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenantRepository _tenantRepository;

        public HttpTenantProvider(IHttpContextAccessor httpContextAccessor, ITenantRepository tenantRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _tenantRepository = tenantRepository;
        }

        public Tenant GetTenant()
        {
            var tenantId = GetTenantId();
            if (!string.IsNullOrWhiteSpace(tenantId))
            {
                var tenant = _tenantRepository.Get(tenantId);
                if (tenant != null)
                {
                    string tenantJson = JsonConvert.SerializeObject(tenant);
                    var tenantBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(tenantJson));

                    _httpContextAccessor.HttpContext.Request.Headers[Constants.Headers.TenantConfig] = tenantBase64;
                }
                return tenant;
            }
            return null;
        }

        public virtual string GetTenantId()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(Constants.Headers.TenantId, out var tenantHeaderValue))
                {
                    return tenantHeaderValue.ToString();
                }
                if (_httpContextAccessor.HttpContext.Request.Query.TryGetValue(Constants.QueryParams.Tenant, out var tenantParamValue))
                {
                    return tenantParamValue.ToString();
                }
            }
            return null;
        }
    }
}
