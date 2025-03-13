using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SkeletonKit.MultiTenancy.Abstractions.Repositories;
using System.Text;

namespace SkeletonKit.MultiTenancy
{
    internal class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITenantRepository _tenantRepository;

        public TenantMiddleware(RequestDelegate next, ITenantRepository tenantRepository)
        {
            _next = next;
            _tenantRepository = tenantRepository;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(Constants.Headers.TenantId, out StringValues tenantIdValues))
            {
                var tenantId = tenantIdValues.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(tenantId))
                {
                    var tenant = _tenantRepository.Get(tenantId);

                    if (tenant != null)
                    {
                        string tenantJson = JsonConvert.SerializeObject(tenant);
                        var tenantBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(tenantJson));

                        context.Request.Headers[Constants.Headers.TenantConfig] = tenantBase64;
                    }

                }
            }
            await _next(context);
        }
    }

    public static class TenantMiddlewareExtensions
    {
        public static void UseTenantMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantMiddleware>();
        }
    }
}
