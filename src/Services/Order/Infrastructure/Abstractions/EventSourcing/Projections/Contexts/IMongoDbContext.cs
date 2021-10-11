using MongoDB.Driver;

namespace Infrastructure.Abstractions.EventSourcing.Projections.Contexts
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}