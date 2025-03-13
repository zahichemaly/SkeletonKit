using SkeletonKit.VersionManager.Abstractions.Repositories;
using SkeletonKit.VersionManager.Models;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace SkeletonKit.VersionManager.Mongo.Repositories
{
    internal class MongoAppVersionRepository : IAppVersionRepository
    {
        private readonly IMongoCollection<AppVersion> _collection;

        public MongoAppVersionRepository(IMongoDbContext mongoDbContext)
        {
            _collection = mongoDbContext.GetCollection<AppVersion>();
        }

        public async Task<List<AppVersion>> GetAllAsync()
        {
            var filter = Builders<AppVersion>.Filter.Empty;
            var query = await _collection.FindAsync(filter);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<AppVersion> GetAsync(object id)
        {
            var filter = Builders<AppVersion>.Filter.Eq(x => x.Id, Convert.ToInt32(id));
            var query = await _collection.FindAsync(filter);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateAsync(AppVersion appVersion)
        {
            var filter = Builders<AppVersion>.Filter.Eq(x => x.Id, appVersion.Id);
            await _collection.ReplaceOneAsync(filter, appVersion);
        }

        public async Task<AppVersion> GetAppVersionByOsAsync(string osName)
        {
            var filter = Builders<AppVersion>.Filter.Eq(x => x.OsName, osName.ToUpperInvariant());
            var query = await _collection.FindAsync(filter);
            var result = await query.FirstOrDefaultAsync();
            return result;
        }
    }
}
