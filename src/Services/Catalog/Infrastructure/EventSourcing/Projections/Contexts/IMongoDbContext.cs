using MongoDB.Driver;

namespace Infrastructure.EventSourcing.Catalogs.Projections.Contexts
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}