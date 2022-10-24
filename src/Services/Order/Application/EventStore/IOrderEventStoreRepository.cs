using Application.Abstractions.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;

namespace Application.EventStore;

public interface IOrderEventStoreRepository : IEventStoreRepository<Order, OrderStoreEvent, OrderSnapshot> { }