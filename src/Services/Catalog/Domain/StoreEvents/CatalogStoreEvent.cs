using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record CatalogStoreEvent : StoreEvent<Guid, Catalog>;