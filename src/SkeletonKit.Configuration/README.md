# 💀 SkeletonKit Configuration

## Description
Configure envionment variables dynamically at runtime either by reading them from the pre-defined environment variables, or from app settings.  

## Usage
1. Create a settings class that will populate the environment variables. It must implement `IConfig`:
 ```  
public sealed class MongoSettings : IConfig
{
    [EnvironmentVariable("MONGO_CONNECTION")]
    public string MongoConnection { get; set; }

    [EnvironmentVariable("MONGO_DBNAME")]
    public string MongoDatabaseName { get; set; }
}
```

This will be mapped to the following section in the `AppSettings.json`:
```
  "MongoSettings": {
    "MongoConnection": "mongodb://localhost:27017/",
    "MongoDatabaseName": "myapp"
  }
```
but will override any environment variables with the following names:
```
"MONGO_CONNECTION": "mongodb://localhost:27017/"
"MONGO_DBNAME": "myapp"
```
that are either defined in the `launchSettings.json` or as secrets in your `.yml` files.

⚠️ Note that environment variables take priority!

2. Inject it in the startup:
```
var mongoSettings = configuration.GetConfig<MongoSettings>();

// add the settings as singleton to be used globally...
services.AddSingleton<MongoSettings>(mongoSettings);

// other DIs
```
