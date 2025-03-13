using Serilog.Core;
using Serilog.Events;

namespace CME.Common.Logging
{
    internal class AddPropertiesEnricher : ILogEventEnricher
    {
        private Dictionary<string, object> _contextInfo;

        public AddPropertiesEnricher(Dictionary<string, object> contextInfo)
        {
            _contextInfo = contextInfo;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            foreach (var property in _contextInfo)
            {
                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty($"{property.Key}", property.Value));
            }
        }
    }
}
