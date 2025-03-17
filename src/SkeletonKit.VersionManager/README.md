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

## Error responses

Error responses have the following structure:

#### Force update
Status code: 416

Response:
```
{
    "code": "force_update",
    "message": "Your application version is no longer supported! Check your store for update."
}
```

#### Maintenance
Status Code: 503

Response:
```
{
    "serverUpExpectedDate": "2026-09-25T10:28:55", // can be used by front-end to show the expected date when the server will be up again
    "code": "service_unavailable",
    "message": "Server is currently down for maintenance."
}
```


## Front-end integration
* Pass `X-Os-Name` as HTTP header with the OS name value (eg: "ANDROID" or "IOS").
* Pass `X-App-Version` as HTTP header with the app version number (eg: "1.5.3").
