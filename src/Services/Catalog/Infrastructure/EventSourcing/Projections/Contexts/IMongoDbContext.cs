using MongoDB.Driver;

namespace Infrastructure.EventSourcing.Projections.Contexts
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}