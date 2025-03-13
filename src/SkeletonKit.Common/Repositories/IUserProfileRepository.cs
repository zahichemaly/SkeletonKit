using SkeletonKit.Common.Entities;

namespace SkeletonKit.Common.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> Get(string id);
        Task Add(UserProfile profile);
        Task UpdatePic(string userId, string imageUrl);
    }
}
