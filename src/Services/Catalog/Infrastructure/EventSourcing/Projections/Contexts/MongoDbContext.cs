using Infrastructure.DependencyInjection.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.EventSourcing.Projections.Contexts
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbOptions _options;

        public MongoDbContext(IOptionsSnapshot<MongoDbOptions> optionsSnapshot)
        {
            _options = optionsSnapshot.Value;

            _database = new MongoClient(new MongoUrl(_options.ConnectionStringFormed))
                .GetDatabase(_options.Database);
        }

        public IMongoCollection<T> GetCollection<T>()
            => _database.GetCollection<T>(typeof(T).Name);
    }
}