using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;

namespace CME.Common.Logging
{
    public static class LoggingExtensions
    {
        public static void AddSerilog(this IServiceCollection services, IConfiguration configuration, List<ILogEventEnricher> enrichers = null)
        {
            Log.Logger = GetSerilog(configuration, enrichers);
            services.AddLogging(x => x.AddSerilog(Log.Logger, dispose: true));
        }

        private static Logger GetSerilog(IConfiguration configuration, List<ILogEventEnricher> enrichers = null)
        {
            var microsoftLogLevel = LogEventLevel.Warning;

            var loggerConfig = new LoggerConfiguration().Enrich.FromLogContext();
            if (enrichers is not null)
            {
                foreach (var enricher in enrichers)
                {
                    loggerConfig = loggerConfig.Enrich.With(enricher);
                }
            }

            return loggerConfig
                .Enrich.WithCorrelationIdHeader("CorrelationId")
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers())
                .MinimumLevel.Override("Microsoft", microsoftLogLevel)
                .MinimumLevel.Override("System", microsoftLogLevel)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static void UseCustomEnricherSerilog(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<CustomEnricherSerilogMiddleware>();
        }
    }
}
