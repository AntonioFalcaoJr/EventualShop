using Application.Abstractions.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore.Events;

public record CatalogSnapshot : Snapshot<Catalog, Guid>;