using CME.MultiTenancy.Abstractions.Providers;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDbGenericRepository;

namespace CME.Transaction.Mongo
{
    public abstract class BaseMongoDBCommand<TRequest, TResponse> : IDBCommand<TRequest, TResponse>
        where TRequest : ICommandRequest<TResponse>
    {
        protected readonly ILogger<BaseMongoDBCommand<TRequest, TResponse>> _logger;
        protected readonly IMongoClient _client;
        protected readonly ITenantProvider _tenantProvider;

        protected BaseMongoDBCommand(ILoggerFactory loggerFactory,
            IMongoDbContext mongoDbContext,
            ITenantProvider tenantProvider)
        {
            _logger = loggerFactory.CreateLogger<BaseMongoDBCommand<TRequest, TResponse>>();
            _client = mongoDbContext.Client;
            _tenantProvider = tenantProvider;
        }

        public async Task<TResponse> Execute(TRequest request)
        {
            using (var session = _client.StartSession())
            {
                session.StartTransaction();
                try
                {
                    _logger.LogInformation("Started MongoCommand");
                    var result = await Execute(request, session);
                    _logger.LogInformation("Finished MongoCommand");
                    session.CommitTransaction();
                    return result;

                }
                catch (Exception ex)
                {
                    _logger.LogError("MongoCommand failed: {Message}", ex.Message);
                    throw;
                }
            }
        }

        public abstract Task<TResponse> Execute(TRequest request, IClientSessionHandle session);
    }
}
