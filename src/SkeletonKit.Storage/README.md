# 💀 SkeletonKit Storage

## Description
Generic storage support for your preferred storage solution, including local, FTP or Azure Storage Account.

## Usage

```
services.AddStorage(Configuration);
```

Configuration in your `AppSettings.json` (or environment variables)

#### Local
```
"StorageSettings": {
    "Type": "local",
    "FilePath": "C:\\Files"
}
```

#### FTP
```
"StorageSettings": {
    "Type": "ftp",
    "FtpHost": "ftphost",
    "FtpUsername": "username",
    "FtpPassword": "xxxxxxxxx"
}
```

#### Azure
```
"StorageSettings": {
    "Type": "azure",
    "AccountUrl": "https://examplestorage.blob.core.windows.net",
    "AccountName": "examplestorage",
    "AccountKey": "xxxxxxxxx", // primary or secondary key to access the storage
    "ContainerName": "files" // optional, in case we want to put our files in a specific blob
}
```
        
#### Using enviroment variables:
```
// Local
STORAGE_FILE_PATH

// FTP
STORAGE_FTP_HOST
STORAGE_FTP_USERNAME
STORAGE_FTP_PASSWORD

// Azure
STORAGE_TYPE
STORAGE_ACCOUNT_URL
STORAGE_ACCOUNT_NAME
STORAGE_ACCOUNT_KEY
STORAGE_CONTAINER_NAME

```

## Dependencies
* [SkeletonKit.Configuration](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.Configuration)
* [SkeletonKit.MultiTenancy](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.MultiTenancy)
