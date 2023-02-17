using MongoDB.Driver;

namespace Infrastructure.Projections.Abstractions;

public interface IMongoDbContext
{
    IMongoCollection<T> GetCollection<T>();
}