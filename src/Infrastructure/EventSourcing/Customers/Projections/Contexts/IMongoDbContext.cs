using MongoDB.Driver;

namespace Infrastructure.EventSourcing.Customers.Projections.Contexts
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}