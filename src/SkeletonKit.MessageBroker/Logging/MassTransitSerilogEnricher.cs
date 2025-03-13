using MassTransit;
using Serilog.Core;
using Serilog.Enrichers.MassTransit;
using Serilog.Events;

namespace CME.MessageBroker.Logging
{
    public class MassTransitSerilogEnricher : ILogEventEnricher
    {
        public MassTransitSerilogEnricher()
        {
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            SerilogEnricher serilogEnricher = new SerilogEnricher();
            serilogEnricher.Enrich(logEvent, propertyFactory);
            if (logEvent.Properties.ContainsKey("MassTransit"))
            {
                var currentPipe = AsyncLocalStack<PipeContext>.Current;
                var context = currentPipe?.GetPayload<MessageContext>();
                if (context != null)
                {
                    logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("CorrelationId", context.CorrelationId));
                    if (context.Headers.TryGetHeader("userId", out object userId))
                    {
                        logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty("userId", userId));
                    }
                }
            }
        }
    }
}
