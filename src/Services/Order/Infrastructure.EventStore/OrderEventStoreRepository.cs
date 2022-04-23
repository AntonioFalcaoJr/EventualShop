using Application.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class OrderEventStoreRepository : EventStoreRepository<Order, OrderStoreEvent, OrderSnapshot, Guid>, IOrderEventStoreRepository
{
    public OrderEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}