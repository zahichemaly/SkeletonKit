# 💀 SkeletonKit Message Broker

## Description
Generic message broker support for RabbitMQ or Azure Service Bus.

## Usage
1. Add the message broker by passing a list of `Type` representing the consumers:
```  
List<Type> types = new List<Type>
{
    typeof(NotificationPayloadConsumer)
};

services.AddMessageBroker(configuration, types);
```

2. Create a section in your app settings:
   
* RabbitMQ:
```
"MessageBrokerSettings": {
  "Host": "rabbitmq://localhost/my_vhost",
  "Username": "guest",
  "Password": "guest",
  "Transport": "rabbitmq",
}
```

* Azure Service Bus:
```
"MessageBrokerSettings": {
  "Host": "sb://my-bus.servicebus.windows.net",
  "Connection": "Endpoint=sb://my-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=XXXXXXXXXXXXXXXXXXXXXXXX",
  "Transport": "azureservicebus",
}
```

or using the following environment variables:
```
MESSAGE_BROKER_CONNECTION
MESSAGE_BROKER_HOST
MESSAGE_BROKER_USERNAME
MESSAGE_BROKER_PASSWORD
MESSAGE_BROKER_TRANSPORT
```

## Dependencies
* [SkeletonKit.Configuration](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.Configuration)
* [SkeletonKit.MultiTenancy](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.MultiTenancy)
