using SkeletonKit.VersionManager.Abstractions.Repositories;
using SkeletonKit.VersionManager.Abstractions.Services;
using SkeletonKit.VersionManager.Exceptions;
using SkeletonKit.VersionManager.Extensions;
using SkeletonKit.VersionManager.Models;

namespace SkeletonKit.VersionManager.Services
{
    /// <summary>
    /// This class implements the interface IAppVersionService. It represents the service responsible of managing the VersionMange dashboard
    /// </summary>
    internal class AppVersionService : IAppVersionService
    {
        private readonly IAppVersionRepository _appVersionRepository;
        private readonly ICacheService _cacheService;

        public AppVersionService(IAppVersionRepository appVersionRepository, ICacheService cacheService)
        {
            _appVersionRepository = appVersionRepository;
            _cacheService = cacheService;
        }

        /// <summary>
        /// This method gets an app version by its id
        /// </summary>
        /// <param name="id">The id of the app version to get</param>
        /// <returns>Returns the app version related to the given id</returns>
        public async Task<AppVersionViewModel> GetAppVersionAsync(int id)
        {
            var appVersion = await _appVersionRepository.GetAsync(id);
            var appVersionViewModel = new AppVersionViewModel()
            {
                Id = appVersion.Id,
                OsName = appVersion.OsName,
                ExceptionsVersions = appVersion.ExceptionsVersions,
                LatestVersion = appVersion.LatestVersion,
                MinimumSupportedVersion = appVersion.MinimumSupportedVersion,
                UnsupportedVersions = appVersion.UnsupportedVersions,
                UseMinimumSupportedVersion = appVersion.UseMinimumSupportedVersion
            };
            return appVersionViewModel;
        }

        /// <summary>
        /// This method gets all app versions to be shown on the Dashboard
        /// </summary>
        /// <returns>Returns all app versions</returns>
        public async Task<IEnumerable<AppVersionViewModel>> GetAppVersionListAsync()
        {
            var appVersionList = await _appVersionRepository.GetAllAsync();
            var appVersionListToReturn = new List<AppVersionViewModel>();
            foreach (var record in appVersionList)
            {
                appVersionListToReturn.Add(new AppVersionViewModel()
                {
                    Id = record.Id,
                    OsName = record.OsName,
                    ExceptionsVersions = record.ExceptionsVersions,
                    LatestVersion = record.LatestVersion,
                    MinimumSupportedVersion = record.MinimumSupportedVersion,
                    UnsupportedVersions = record.UnsupportedVersions,
                    UseMinimumSupportedVersion = record.UseMinimumSupportedVersion
                });
            }
            return appVersionListToReturn;
        }

        /// <summary>
        /// This method checks whether the given version is supported or not.
        /// </summary>
        /// <param name="versionStr">The version to check</param>
        /// <param name="osName">The operating system name where the app is running</param>
        /// <returns>Returns an object containing a boolean value with the latest available version</returns>
        public async Task<SupportedVersion> IsSupportedVersionAsync(string versionStr, string osName)
        {
            SupportedVersion supportedVersion = new SupportedVersion();

            var appVersion = await _cacheService.GetAsync($"AppVersion-{osName}", TimeSpan.FromDays(1).Days, async () => await _appVersionRepository.GetAppVersionByOsAsync(osName));
            if (appVersion == null)
                throw new InvalidOsNameException();
            if (appVersion.MaintenanceTime != null && appVersion.MaintenanceTime >= DateTime.UtcNow)
            {
                supportedVersion.MaintenanceTime = appVersion.MaintenanceTime;
                return supportedVersion;
            }
            Version version = null;
            if (!Version.TryParse(versionStr, out version))
                throw new VersionFormatException();
            if (string.IsNullOrWhiteSpace(osName))
                supportedVersion.Supported = false;

            if (appVersion.UseMinimumSupportedVersion)
            {
                Version minimumSupportedVersion = null;
                Version.TryParse(appVersion.MinimumSupportedVersion, out minimumSupportedVersion);
                if (version >= minimumSupportedVersion)
                    supportedVersion.Supported = true;
                else
                {
                    supportedVersion.Supported = version.Exists(appVersion.ExceptionsVersions.AsVersionList());
                }
            }
            else
            {
                supportedVersion.Supported = !version.Exists(appVersion.UnsupportedVersions.AsVersionList());
            }
            supportedVersion.LatestVersion = appVersion.LatestVersion;
            return supportedVersion;
        }

        /// <summary>
        /// This method checks if an app version is valid in terms of version format which should be as follow: x.y.z.w
        /// </summary>
        /// <param name="appVersionViewModel">The app version to validate</param>
        /// <returns>Returns a boolean indicating whether the app version is valid or not</returns>
        public bool IsValid(AppVersionViewModel appVersionViewModel)
        {
            if (!string.IsNullOrWhiteSpace(appVersionViewModel.ExceptionsVersions))
                if (!appVersionViewModel.ExceptionsVersions.IsValidVersionsJsonFormat())
                    return false;
            if (!string.IsNullOrWhiteSpace(appVersionViewModel.LatestVersion))
                if (!appVersionViewModel.LatestVersion.IsValidVersionFormat())
                    return false;
            if (appVersionViewModel.UseMinimumSupportedVersion)
                if (!appVersionViewModel.MinimumSupportedVersion.IsValidVersionFormat())
                    return false;
            if (!string.IsNullOrWhiteSpace(appVersionViewModel.UnsupportedVersions))
                if (!appVersionViewModel.UnsupportedVersions.IsValidVersionsJsonFormat())
                    return false;
            return true;
        }

        /// <summary>
        /// This method updates an app version edited by the dashboard user.
        /// </summary>
        /// <param name="appVersionViewModel">The app version to update</param>
        /// <param name="ct">The cancellation token</param>
        /// <param name="user">The logged in user that is updating the app version</param>
        /// <returns></returns>
        public async Task UpdateAppVersionAsync(AppVersionViewModel appVersionViewModel, string user)
        {
            var appVersionRecord = await _appVersionRepository.GetAsync(appVersionViewModel.Id);
            appVersionRecord.LatestVersion = appVersionViewModel.LatestVersion;
            appVersionRecord.MinimumSupportedVersion = appVersionViewModel.MinimumSupportedVersion;
            appVersionRecord.UnsupportedVersions = appVersionViewModel.UnsupportedVersions;
            appVersionRecord.UseMinimumSupportedVersion = appVersionViewModel.UseMinimumSupportedVersion;
            appVersionRecord.ExceptionsVersions = appVersionViewModel.ExceptionsVersions;
            appVersionRecord.UpdatedOn = DateTime.UtcNow;
            appVersionRecord.UpdatedBy = user;
            await _appVersionRepository.UpdateAsync(appVersionRecord);
        }
    }
}
