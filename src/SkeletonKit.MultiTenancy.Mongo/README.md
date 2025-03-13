# 💀 SkeletonKit Multi-Tenancy Mongo

## Description
MongoDB implementation of [SkeletonKit.MultiTenancy](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.MultiTenancy).

## Usage
* Basic tenant support:
```
services
    .AddMultiTenancySupport<HttpTenantProvider>(configuration)
    .AddCachedMongo();
```


* Message Broker tenant support (requires [SkeletonKit.MessageBroker](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.MessageBroker))
```
services
    .AddMultiTenancySupport<MessageBrokerTenantProvider>(configuration)
    .AddCachedMongo();
```

## Dependencies
* [SkeletonKit.MultiTenancy](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.MultiTenancy)
