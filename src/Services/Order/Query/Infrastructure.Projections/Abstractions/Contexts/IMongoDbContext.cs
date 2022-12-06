using MongoDB.Driver;

namespace Infrastructure.Projections.Abstractions.Contexts;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>();
}