using Application.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class OrderEventStoreRepository : EventStoreRepository<Order, OrderStoreEvent, OrderSnapshot>, IOrderEventStoreRepository
{
    public OrderEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}