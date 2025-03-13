using CME.MessageBroker.Configurations;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace CME.MessageBroker.Hosting
{
    public class BusClient : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly string _hostUrl;

        public BusClient(IBusControl busControl, MessageBrokerSettings messageBrokerSettings)
        {
            _busControl = busControl;
            _hostUrl = messageBrokerSettings.Host;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }

        public async Task Send<TContract>(string queueName, object data, Dictionary<string, string> headers = null) where TContract : class
        {
            var sendEndpoint = await _busControl.GetSendEndpoint(new Uri(GetEndpointFullUrl(queueName)));
            await sendEndpoint.Send<TContract>(data, x =>
            {
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        x.Headers.Set(item.Key, item.Value);
                    }
                }
            });
        }

        public async Task Send<TContract>(string queueName, object data, string faultAddress) where TContract : class
        {
            var sendEndpoint = await _busControl.GetSendEndpoint(new Uri(GetEndpointFullUrl(queueName)));
            await sendEndpoint.Send<TContract>(data, c => c.FaultAddress = new Uri(GetEndpointFullUrl(faultAddress)));
        }

        public async Task Publish<TContract>(object data, Dictionary<string, string> headers = null) where TContract : class
        {
            await _busControl.Publish<TContract>(data, x =>
            {
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        x.Headers.Set(item.Key, item.Value);
                    }
                }
            });
        }

        public async Task Publish<TContract>(object data, string faultAddress) where TContract : class
        {
            await _busControl.Publish<TContract>(data, c => c.FaultAddress = new Uri(GetEndpointFullUrl(faultAddress)));
        }

        private string GetEndpointFullUrl(string queueName)
        {
            queueName = queueName.Replace("Consumer", "");
            string fullUri = string.Join("/", _hostUrl, queueName);
            return fullUri;
        }
    }
}
