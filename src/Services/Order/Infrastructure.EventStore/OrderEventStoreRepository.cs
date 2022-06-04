using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class OrderEventStoreRepository : EventStoreRepository<Order, StoreEvents.Event, StoreEvents.Snapshot, Guid>, IOrderEventStoreRepository
{
    public OrderEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}