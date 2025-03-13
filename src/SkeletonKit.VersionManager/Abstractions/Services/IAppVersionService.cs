using CME.VersionManager.Models;

namespace CME.VersionManager.Abstractions.Services
{
    /// <summary>
    /// This interface represents the service responsible of managing the VersionMange dashboard
    /// </summary>
    public interface IAppVersionService
    {
        /// <summary>
        /// This method gets all app versions to be shown on the Dashboard
        /// </summary>
        /// <returns>Returns all app versions</returns>
        Task<IEnumerable<AppVersionViewModel>> GetAppVersionListAsync();
        /// <summary>
        /// This method updates an app version edited by the dashboard user.
        /// </summary>
        /// <param name="appVersionViewModel">The app version to update</param>
        /// <param name="user">The logged in user that is updating the app version</param>
        /// <returns></returns>
        Task UpdateAppVersionAsync(AppVersionViewModel appVersionViewModel, string user);
        //Task DeleteAppVersionAsync(AppVersionViewModel appVersionViewModel, CancellationToken ct);
        //Task CreateAppVersionAsync(AppVersionViewModel appVersionViewModel, CancellationToken ct, string user);
        /// <summary>
        /// This method gets an app version by its id
        /// </summary>
        /// <param name="id">The id of the app version to get</param>
        /// <returns>Returns the app version related to the given id</returns>
        Task<AppVersionViewModel> GetAppVersionAsync(int id);
        /// <summary>
        /// This method checks if an app version is valid in terms of version format which should be as follow: x.y.z.w
        /// </summary>
        /// <param name="appVersionViewModel">The app version to validate</param>
        /// <returns>Returns a boolean indicating whether the app version is valid or not</returns>
        bool IsValid(AppVersionViewModel appVersionViewModel);
        /// <summary>
        /// This method checks whether the given version is supported or not.
        /// </summary>
        /// <param name="versionStr">The version to check</param>
        /// <param name="osName">The operating system name where the app is running</param>
        /// <returns>Returns an object containing a boolean value with the latest available version</returns>
        Task<SupportedVersion> IsSupportedVersionAsync(string versionStr, string osName);
    }
}
