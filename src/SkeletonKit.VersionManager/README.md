# 💀 SkeletonKit Version Manager

## Description
App version manager support. Returns special error codes to the front-end so they can handle force update or maintenance screen scenarios, based on the OS name and app version that should be passed as headers.

## Usage

```
services.AddVersionManager<TCacheService, TAppVersionRepository>()
```

where `TCacheService` and `TAppVersionRepository` is our own implementation of `ICacheService` and `IAppVersionRepository` respectively.

After building the service:

```
app.UseVersionManager();
```

## Front-end integration
* Pass `X-Os-Name` as HTTP header with the OS name value (eg: "ANDROID" or "IOS").
* Pass `X-App-Version` as HTTP header with the app version number (eg: "1.5.3").
