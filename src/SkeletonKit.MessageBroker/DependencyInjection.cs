using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SkeletonKit.Configuration;
using SkeletonKit.MessageBroker.Configurations;
using SkeletonKit.MessageBroker.Extensions;
using SkeletonKit.MessageBroker.Hosting;

namespace SkeletonKit.MessageBroker
{
    public static class DependencyInjection
    {
        public static void AddMessageBroker(this IServiceCollection services,
            IConfiguration configuration,
            IEnumerable<Type> consumersType,
            Action<IBusFactoryConfigurator, IServiceProvider> configurator = null)
        {
            var settings = configuration.GetConfig<MessageBrokerSettings>();
            services.AddMassTransit(x =>
            {
                if (consumersType != null)
                {
                    foreach (var type in consumersType)
                    {
                        x.AddConsumer(type);
                    }
                }

                if (settings.Transport.Equals(MessageBrokerSettings.TRANSPORTS.RabbitMQ))
                {
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(new Uri(settings.Host), hst =>
                        {
                            hst.Username(settings.Username);
                            hst.Password(settings.Password);
                        });

                        cfg.ConfigureEndpoints(context);

                        // mutli-tenancy
                        cfg.UseMultiTenancy(context);

                        // serialization
                        cfg.UseNewtonsoftJsonSerializer();
                        cfg.UseNewtonsoftJsonDeserializer();

                        // custom config if available
                        configurator?.Invoke(cfg, context);
                    });
                }
                else if (settings.Transport.Equals(MessageBrokerSettings.TRANSPORTS.AzureServiceBus))
                {
                    x.UsingAzureServiceBus((context, cfg) =>
                    {
                        cfg.Host(settings.ConnectionString, hostConfig =>
                        {
                            hostConfig.RetryLimit = 5;
                        });

                        cfg.ConfigureEndpoints(context);

                        cfg.EnableDuplicateDetection(TimeSpan.FromSeconds(20));

                        // mutli-tenancy
                        cfg.UseMultiTenancy(context);

                        // serialization
                        cfg.UseNewtonsoftJsonSerializer();
                        cfg.UseNewtonsoftJsonDeserializer();

                        // custom config if available
                        configurator?.Invoke(cfg, context);
                    });
                }
                else
                {
                    throw new ApplicationException("Unsupported transport type " + settings.Transport);
                }
            });

            services.AddSingleton<MessageBrokerSettings>(settings);
            services.AddSingleton<IHostedService, BusClient>();
            services.AddSingleton<BusClient>();
        }
    }
}
