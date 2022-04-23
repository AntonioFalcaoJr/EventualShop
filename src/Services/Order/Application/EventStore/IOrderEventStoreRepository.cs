using Application.Abstractions.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IOrderEventStoreRepository : IEventStoreRepository<Order, OrderStoreEvent, OrderSnapshot, Guid> { }