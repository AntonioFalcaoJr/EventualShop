using Application.Abstractions.EventStore;
using Domain;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IOrderEventStoreRepository : IEventStoreRepository<Order, StoreEvents.Event, StoreEvents.Snapshot, Guid> { }