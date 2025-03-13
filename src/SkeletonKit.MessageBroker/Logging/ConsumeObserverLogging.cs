using MassTransit;
using Newtonsoft.Json;
using Serilog;

namespace CME.MessageBroker.Logging
{
    internal class ConsumeObserverLogger : IConsumeObserver
    {
        private readonly ILogger _logger;

        public ConsumeObserverLogger(ILogger logger)
        {
            _logger = logger;
        }

        Task IConsumeObserver.PreConsume<T>(ConsumeContext<T> context)
        {
            var message = JsonConvert.SerializeObject(context.Message);
            ILogger log = GetMassTransitContext(context);
            log = log.ForContext("Payload", message);
            log.Information("A message is before the consume stage");
            return Task.FromResult(false);
        }

        Task IConsumeObserver.PostConsume<T>(ConsumeContext<T> context)
        {
            ILogger log = GetMassTransitContext(context);
            log.Information("A message is past the consume stage");
            return Task.FromResult(false);
        }

        Task IConsumeObserver.ConsumeFault<T>(ConsumeContext<T> context, Exception exception)
        {
            ILogger log = GetMassTransitContext(context);
            if (exception != null)
            {
                string stackTrace = exception?.GetBaseException() != null ?
                      exception.GetBaseException()?.StackTrace :
                      exception?.StackTrace;
                log = log.ForContext("StackTrace", stackTrace);
            }
            log.Error("A fault is consumed exception {message}", exception.Message);
            return Task.FromResult(false);
        }

        private ILogger GetMassTransitContext<T>(ConsumeContext<T> context) where T : class
        {
            var log = _logger.ForContext("CorrelationId", context.CorrelationId);
            log = log.ForContext("DestinationAddress", context.DestinationAddress);
            if (context.Headers.TryGetHeader("userId", out object userId))
            {
                log = log.ForContext("userId", userId.ToString());
            }

            return log;
        }
    }
}
