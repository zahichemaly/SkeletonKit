using SkeletonKit.MultiTenancy.Abstractions.Repositories;
using SkeletonKit.MultiTenancy.Entities;
using SkeletonKit.MultiTenancy.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SkeletonKit.MultiTenancy.Mongo.Repositories
{
    internal class MongoTenantRepository : ITenantRepository
    {
        private readonly IMongoCollection<Tenant> _collection;
        private readonly IMongoDatabase _database;
        private readonly LocalTenantRepository _localTenantRepository;

        public MongoTenantRepository(IMongoClient mongoClient, LocalTenantRepository localTenantRepository)
        {
            _database = mongoClient.GetDatabase(Constants.DB_NAME);
            _collection = _database.GetCollection<Tenant>(Constants.TENANT_COLLECTION_NAME);
            _localTenantRepository = localTenantRepository;
            Initialize();
        }

        public Tenant Get(string id)
        {
            var filter = Builders<Tenant>.Filter.Eq(x => x.Name, id.ToLowerInvariant());
            var query = _collection.Find(filter);
            var result = query.FirstOrDefault();
            return result;
        }

        public IEnumerable<Tenant> GetAll()
        {
            var filter = Builders<Tenant>.Filter.Empty;
            var query = _collection.Find(filter);
            var result = query.ToList();
            return result;
        }

        public void Initialize()
        {
            if (!CollectionExists())
            {
                var localTenants = _localTenantRepository.GetAll();
                if (localTenants != null && localTenants.Any())
                {
                    _collection.InsertMany(localTenants);
                }
            }
        }

        private bool CollectionExists()
        {
            var filter = new BsonDocument();
            var totalCount = _collection.CountDocuments(filter);
            return totalCount > 0;
        }
    }
}
