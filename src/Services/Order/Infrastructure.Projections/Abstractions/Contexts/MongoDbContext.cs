using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Projections.Abstractions.Contexts;

public abstract class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    protected MongoDbContext(IConfiguration configuration)
    {
        var mongoUrl = new MongoUrl(configuration.GetConnectionString("Projections"));
        _database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>()
        => _database.GetCollection<T>(typeof(T).Name);
}