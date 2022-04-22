using Application.Abstractions.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore.Events;

public record CatalogStoreEvent : StoreEvent<Catalog, Guid>;