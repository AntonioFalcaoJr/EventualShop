using Application.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class ShoppingCartEventStoreRepository : EventStoreRepository<ShoppingCart, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid>, IShoppingCartEventStoreRepository
{
    public ShoppingCartEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}