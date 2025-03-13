using CME.MessageBroker.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CME.MessageBroker.Logging
{
    internal class MessageBrokerLoggerProvider : ILoggerProvider
    {
        private readonly IHttpContextAccessor _accessor;
        private MessageBrokerLogger _messageBrokeredLogger;
        private readonly BusClient _bus;
        private string _loggingQueueName;

        public MessageBrokerLoggerProvider(IHttpContextAccessor accessor,
            BusClient bus,
            string loggingQueueName)
        {
            _accessor = accessor;
            _bus = bus;
            _loggingQueueName = loggingQueueName;
        }

        public ILogger CreateLogger(string categoryName)
        {
            _messageBrokeredLogger = new MessageBrokerLogger(categoryName, _accessor, _bus, _loggingQueueName);
            return _messageBrokeredLogger;
        }
        public void Dispose()
        {
        }
    }
}
