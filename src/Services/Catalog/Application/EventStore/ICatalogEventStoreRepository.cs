using Application.Abstractions.EventStore;
using Domain;
using Domain.Aggregates;

namespace Application.EventStore;

public interface ICatalogEventStoreRepository : IEventStoreRepository<Catalog, StoreEvents.Event, StoreEvents.Snapshot, Guid> { }