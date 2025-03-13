using SkeletonKit.VersionManager.Models;

namespace SkeletonKit.VersionManager.Abstractions.Repositories
{
    /// <summary>
    /// This interface provides methods to access the AppVersion table.
    /// </summary>
    public interface IAppVersionRepository
    {
        /// <summary>
        /// This method gets all available app version from the database
        /// </summary>
        /// <returns>Returns all available app version</returns>
        Task<List<AppVersion>> GetAllAsync();
        /// <summary>
        /// This method gets an app version from the database based on its id
        /// </summary>
        /// <param name="id">The id of the app version to get</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>Returns the app version for the given id</returns>
        Task<AppVersion> GetAsync(object Id);

        //Task AddAsync(AppVersion entity, CancellationToken ct);
        /// <summary>
        /// This method updates an app version given as parameter
        /// </summary>
        /// <param name="appVersion">The app version to update</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns></returns>
        Task UpdateAsync(AppVersion entity);

        //Task RemoveAsync(object Id, CancellationToken ct);
        /// <summary>
        /// This method gets an app version from the database based on the operating system name
        /// </summary>
        /// <param name="osName">The operating system name</param>
        /// <returns>Returns the app version related to the given operating system name</returns>
        Task<AppVersion> GetAppVersionByOsAsync(string osName);
    }
}
