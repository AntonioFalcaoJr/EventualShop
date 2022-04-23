using Application.Abstractions.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore;

public interface ICatalogEventStoreRepository : IEventStoreRepository<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid> { }