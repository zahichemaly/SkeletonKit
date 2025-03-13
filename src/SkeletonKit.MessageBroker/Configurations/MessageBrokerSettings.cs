using CME.Configuration;

namespace CME.MessageBroker.Configurations
{
    public sealed class MessageBrokerSettings : IConfig
    {
        public static class TRANSPORTS
        {
            public const string RabbitMQ = "rabbitmq";

            public const string AzureServiceBus = "azureservicebus";
        }

        [EnvironmentVariable("MESSAGE_BROKER_CONNECTION")]
        public string ConnectionString { get; set; }

        [EnvironmentVariable("MESSAGE_BROKER_HOST")]
        public string Host { get; set; }

        [EnvironmentVariable("MESSAGE_BROKER_USERNAME")]
        public string Username { get; set; }

        [EnvironmentVariable("MESSAGE_BROKER_PASSWORD")]
        public string Password { get; set; }

        [EnvironmentVariable("MESSAGE_BROKER_TRANSPORT")]
        public string Transport { get; set; }

        [EnvironmentVariable("MESSAGE_BROKER_LOGGING_QUEUE_NAME")]
        public string LoggingQueueName { get; set; }
    }
}
