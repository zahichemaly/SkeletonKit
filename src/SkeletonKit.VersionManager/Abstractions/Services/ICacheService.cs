using SkeletonKit.VersionManager.Models;

namespace SkeletonKit.VersionManager.Abstractions.Services
{
    /// <summary>
    /// This interface provides methods for managing the cache service
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// This method removes data from the cache based on the given cache key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);
        /// <summary>
        /// This method gets the data from the cache based on the given key.
        /// </summary>
        /// <param name="key">The cache key</param>
        /// <param name="expiration">The expiration time</param>
        /// <param name="acquire">The method that needs to be executed in case the data is not in the cache</param>
        /// <param name="expirationInHours">a boolean value indicating whther the expiration given is an hours or in minutes</param>
        /// <returns>Returns the data from the cache</returns>
        Task<AppVersion> GetAsync(string key, int expiration, Func<Task<AppVersion>> acquire, bool expirationInHours = true);
    }
}
