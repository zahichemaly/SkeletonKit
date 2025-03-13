using SkeletonKit.Common;
using SkeletonKit.Common.Logging;
using SkeletonKit.Configuration;
using SkeletonKit.MessageBroker.Configurations;
using SkeletonKit.MessageBroker.Extensions;
using SkeletonKit.MessageBroker.Hosting;
using SkeletonKit.MessageBroker.Logging;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

                        // logger
                        cfg.UseSerilogLogging();

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

                        // logger
                        cfg.UseSerilogLogging();

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

        public static void AddSerilogMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSerilog(configuration, new()
            {
                new MassTransitSerilogEnricher()
            });
        }

        public static void UseMessageBrokeredLogger(this IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            MessageBrokerSettings messageBrokerSettings = serviceProvider.GetRequiredService<MessageBrokerSettings>();
            loggerFactory.AddMessageBrokeredLogger(serviceProvider.GetService<IHttpContextAccessor>(),
                serviceProvider.GetService<BusClient>(),
                messageBrokerSettings.LoggingQueueName);
        }

        private static ILoggerFactory AddMessageBrokeredLogger(this ILoggerFactory factory,
            IHttpContextAccessor accessor,
            BusClient bus,
            string loggingQueueName)
        {
            factory.AddProvider(new MessageBrokerLoggerProvider(accessor, bus, loggingQueueName));
            return factory;
        }


        public static IHealthChecksBuilder EnableMessageBroker(this IHealthChecksBuilder healthChecksBuilder, IConfiguration configuration)
        {
            var messageBrokerSettings = configuration.GetConfig<MessageBrokerSettings>();
            if (messageBrokerSettings.Transport == MessageBrokerSettings.TRANSPORTS.AzureServiceBus)
            {
                healthChecksBuilder.AddAzureServiceBusQueue(connectionString: messageBrokerSettings.ConnectionString, queueName: "entityhistory", tags: new[] { Constants.HealthCheck.Readiness });
            }
            else if (messageBrokerSettings.Transport == MessageBrokerSettings.TRANSPORTS.RabbitMQ)
            {
                //healthChecksBuilder.AddRabbitMQ(rabbitConnectionString: messageBrokerSettings.ConnectionString, tags: new[] { Constants.HealthCheck.Readiness });
            }
            return healthChecksBuilder;
        }
    }
}
