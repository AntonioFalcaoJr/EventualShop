using MongoDB.Driver;

namespace Infrastructure.EventSourcing.Accounts.Projections.Contexts
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>();
    }
}