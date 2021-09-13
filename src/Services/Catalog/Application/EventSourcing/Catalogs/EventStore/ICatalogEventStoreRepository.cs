using System;
using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.Catalogs.EventStore.Events;
using Domain.Entities.Catalogs;

namespace Application.EventSourcing.Catalogs.EventStore
{
    public interface ICatalogEventStoreRepository : IEventStoreRepository<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid> { }
}