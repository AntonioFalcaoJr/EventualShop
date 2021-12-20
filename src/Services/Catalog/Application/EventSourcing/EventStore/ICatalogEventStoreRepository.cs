using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface ICatalogEventStoreRepository : IEventStoreRepository<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid> { }