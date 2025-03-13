using SkeletonKit.Common;
using SkeletonKit.MessageBroker.Logging;
using SkeletonKit.MultiTenancy.Abstractions.Providers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
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
                        c.Headers.Set(Constants.Headers.TenantId, tenant.Name.ToLowerInvariant());

                        string tenantJson = JsonConvert.SerializeObject(tenant);
                        var tenantBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(tenantJson));
                        c.Headers.Set(Constants.Headers.TenantConfig, tenantBase64);
                    }
                }
            }));
        }

        internal static void UseSerilogLogging(this IBusFactoryConfigurator cfg)
        {
            ConsumeObserverLogger consumeObserverLogger = new ConsumeObserverLogger(Log.Logger);
            cfg.ConnectConsumeObserver(consumeObserverLogger);
        }
    }
}
