using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class ShoppingCartEventStoreRepository : EventStoreRepository<ShoppingCart, StoreEvents.Event, StoreEvents.Snapshot, Guid>, IShoppingCartEventStoreRepository
{
    public ShoppingCartEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}