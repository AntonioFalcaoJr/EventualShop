using System;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Entities.Catalogs;

namespace Application.EventSourcing.EventStore.Events
{
    public record CatalogSnapshot : Snapshot<Catalog, Guid>;
}