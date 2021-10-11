using Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Abstractions.EventSourcing.Projections.Contexts
{
    public abstract class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbOptions _options;

        protected MongoDbContext(IOptionsMonitor<MongoDbOptions> optionsSnapshot)
        {
            _options = optionsSnapshot.CurrentValue;

            _database = new MongoClient(new MongoUrl(_options.ConnectionStringFormed))
                .GetDatabase(_options.Database);
        }

        public IMongoCollection<T> GetCollection<T>()
            => _database.GetCollection<T>(typeof(T).Name);
    }
}