# 💀 SkeletonKit 
This is a collection of core components that can help lazy developers easily integrate with several technologies so they can quick off their project faster. 

## Features
The tools include:

✅ File management solution using different strategies (Local, FTP, Azure Storage Account)

✅️ Caching mechanism solution using different strategies (In-memory, Redis)

✅ Message broker solution using different technologies (RabbitMQ, Azure Service Bus) through MassTransit

✅ App versionning solution to push force update or server maintenance to end-users.

## Dependencies
This kit relies on several dependencies:

⭕ [MongoDB C# Driver](https://github.com/mongodb/mongo-csharp-driver)

⭕ [MongoDB Generic Repository](https://github.com/alexandre-spieser/mongodb-generic-repository)

⭕ [MassTransit](https://github.com/MassTransit/MassTransit)

⭕ [CacheManager](https://github.com/MichaCo/CacheManager)

⭕ [CacheManager](https://github.com/MichaCo/CacheManager)


## Libraries overview
This is a list of common modules located in the `Common/` directory:
| Name                          | Description                                                 |
|-------------------------------|-------------------------------------------------------------|
| SkeletonKit.Configuration              | Configure envionment variables dynamically at runtime either by reading them from the pre-defined environment variables, or from app settings.                |
| SkeletonKit.Localization.Mongo         | Localization support for MongoDB.                            |
| SkeletonKit.MessageBroker              | Generic message broker support for RabbitMQ or Azure Service Bus.    |
| SkeletonKit.MultiTenancy               | Generic multi-tenancy support for your preferred database. Relies on a special header value to communicate with the tenant's database.         |
| SkeletonKit.MultiTenancy.Mongo         | MongoDB implementation of MultiTenancy.                  |
| SkeletonKit.Storage                    | Generic storage support for your preferred storage solution, including local, FTP or Azure Storage Account. |
| SkeletonKit.VersionManager             | App version manager support. Returns special error codes to the front-end so they can handle force update or maintenance screen scenarios, based on the OS name and app version that should be passed as headers.      |
| SkeletonKit.VersionManager.Mongo       | MongoDB implementation of VersionManager.                 |

## Documentation
Navigate to the `README` of the module inside the `src/` folder.

## License
This project is licensed under the terms of the [MIT License](LICENSE).
