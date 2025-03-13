using CME.Common;
using CME.Common.Extensions;
using CME.MessageBroker.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CME.MessageBroker.Logging
{
    internal class MessageBrokerLogger : ILogger
    {
        private string _categoryName;
        private readonly IHttpContextAccessor _accessor;
        private readonly BusClient _bus;
        private readonly string _loggingQueueName;

        public MessageBrokerLogger(string categoryName,
            IHttpContextAccessor accessor,
            BusClient bus,
            string loggingQueueName)
        {
            _categoryName = categoryName;
            _accessor = accessor;
            _bus = bus;
            _loggingQueueName = loggingQueueName;

        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string correlationId = Guid.NewGuid().ToString();
            string userId = Constants.System;
            if (_accessor != null && _accessor.HttpContext != null)
            {
                correlationId = _accessor.HttpContext.Request.Headers["CorrelationId"].FirstOrDefault() ?? _accessor.HttpContext.TraceIdentifier;
                userId = _accessor.HttpContext.User?.Claims?.GetValue<string>(Constants.Claims.UserName) ?? Constants.System;
            }

            var pairs = (state as IEnumerable<KeyValuePair<string, object>>)?.ToDictionary(i => i.Key, i => i.Value);

            if (pairs == null) throw new InvalidOperationException("Can't cast state to IEnumerable<KeyValuePair<string, object>>");
            string message = pairs.Last().Value.ToString();
            List<object> objs = pairs.Take(pairs.Count - 1).Select(x => x.Value).ToList();
            string stackTrace = string.Empty;
            if (exception != null)
            {
                stackTrace = exception?.GetBaseException() != null ?
                    exception.GetBaseException().StackTrace :
                    exception.StackTrace;
            }

            var contextInfo = new Dictionary<string, object>();
            if (contextInfo != null && !string.IsNullOrEmpty(stackTrace))
            {
                contextInfo.Add(nameof(Exception.StackTrace), stackTrace);
            }

            await _bus.Send<IWriteLog>(_loggingQueueName,
            new
            {
                Level = (int)logLevel,
                Category = _categoryName,
                Source = string.Empty,
                Data = objs,
                Message = message,
                User = userId,
                CreatedOn = DateTime.UtcNow,
                ContextInfo = contextInfo,
                CorrelationId = correlationId
            });
        }
    }
}
