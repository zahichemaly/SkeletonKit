using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SkeletonKit.MultiTenancy.Abstractions.Providers;
using System.Text;

namespace SkeletonKit.MessageBroker.Extensions
{
    public static class BusFactoryConfiguratorExtensions
    {
        public static void UseMultiTenancy(this IBusFactoryConfigurator cfg, IServiceProvider provider)
        {
            cfg.ConfigureSend(s => s.UseSendExecute(c =>
            {
                using (var scope = provider.CreateScope())
                {
                    var tenantProvider = scope.ServiceProvider.GetRequiredService<ITenantProvider>();
                    var tenant = tenantProvider.GetTenant();
                    if (tenant != null)
                    {
                        c.Headers.Set(MultiTenancy.Constants.Headers.TenantId, tenant.Name.ToLowerInvariant());

                        string tenantJson = JsonConvert.SerializeObject(tenant);
                        var tenantBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(tenantJson));
                        c.Headers.Set(MultiTenancy.Constants.Headers.TenantConfig, tenantBase64);
                    }
                }
            }));
        }
    }
}
