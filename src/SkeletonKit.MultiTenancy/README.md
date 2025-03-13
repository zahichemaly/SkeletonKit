# 💀 SkeletonKit Multi-Tenancy

## Description
Generic multi-tenancy support for your preferred database. Relies on a special header value to communicate with the tenant's database.

## Usage
For MongoDB support, use the [SkeletonKit.MultiTenancy.Mongo](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.MultiTenancy.Mongo).
For other DB supports, create your own implementation of the `Abstractions` package inside the project.

## Front-end integration
Pass `X-TenantId` as HTTP header with the tenant name value.

## Dependencies
* [SkeletonKit.Configuration](https://github.com/zahichemaly/SkeletonKit/tree/master/src/SkeletonKit.Configuration)
