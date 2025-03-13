using SkeletonKit.VersionManager.Abstractions.Services;
using SkeletonKit.VersionManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace SkeletonKit.VersionManager.Helpers
{
    internal class VersionManagerHelper
    {
        public static async Task<VersionValidationResponse> Get(HttpContext context)
        {
            VersionValidationResponse versionValidationResponse = new VersionValidationResponse();
            IAppVersionService appVersionService = context.RequestServices.GetRequiredService<IAppVersionService>();
            StringValues osName;
            if (context.Request.Headers.TryGetValue(Constants.OsNameHeaderKey, out osName))
            {
                StringValues appVersion;
                if (context.Request.Headers.TryGetValue(Constants.AppVersionHeaderKey, out appVersion))
                {
                    var versionSupported = await appVersionService.IsSupportedVersionAsync(appVersion.ToString(), osName.ToString());
                    if (versionSupported.MaintenanceTime != null)
                    {
                        versionValidationResponse.StatusCode = Constants.MaintenanceHttpStatusCode;
                        versionValidationResponse.Error = new MaintenanceError(versionSupported.MaintenanceTime);
                        versionValidationResponse.MaintenanceTime = versionSupported.MaintenanceTime;
                    }
                    else if (!versionSupported.Supported)
                    {
                        versionValidationResponse.StatusCode = Constants.ForceUpdateHttpStatusCode;
                        versionValidationResponse.Error = new Error() { Code = Constants.ForceUpdateCode, Message = Constants.ForceUpdateMessage };
                    }
                }
                else
                {
                    versionValidationResponse.StatusCode = 400;
                    versionValidationResponse.Error = new Error() { Code = Constants.AppVersionHeaderMissingCode, Message = Constants.AppVersionHeaderMissingMessage };

                }
            }
            //else
            //{
            //    versionValidationResponse.StatusCode = 400;
            //    versionValidationResponse.Error = new Error() { ErrorCode = Constants.OsNameHeaderMissingCode, ErrorDescription = Constants.OsNameHeaderMissingMessage };
            //}
            return versionValidationResponse;
        }
    }
}
