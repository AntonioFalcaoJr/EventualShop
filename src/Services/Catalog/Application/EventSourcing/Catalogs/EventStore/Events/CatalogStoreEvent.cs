using System;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Entities.Catalogs;

namespace Application.EventSourcing.Catalogs.EventStore.Events
{
    public record CatalogStoreEvent : StoreEvent<Catalog, Guid>;
}