using SkeletonKit.VersionManager.Helpers;
using SkeletonKit.VersionManager.Models;
using Microsoft.AspNetCore.Http;

namespace SkeletonKit.VersionManager
{
    internal class VersionManagerMiddleware
    {
        private readonly RequestDelegate _next;

        public VersionManagerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            VersionValidationResponse versionValidationResponse = await VersionManagerHelper.Get(context);
            if (versionValidationResponse.IsSupported)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = versionValidationResponse.StatusCode;
                if (versionValidationResponse.IsUnavailable)
                {
                    context.Response.Headers.Add("Retry-After", versionValidationResponse.MaintenanceDisplayTime);
                }
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(versionValidationResponse.Error.ToString());
            }
        }
    }
}
