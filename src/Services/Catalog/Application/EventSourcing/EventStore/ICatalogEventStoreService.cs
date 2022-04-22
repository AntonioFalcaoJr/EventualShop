using Application.Abstractions.EventStore;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface ICatalogEventStoreService : IEventStoreService<Catalog, Guid> { }