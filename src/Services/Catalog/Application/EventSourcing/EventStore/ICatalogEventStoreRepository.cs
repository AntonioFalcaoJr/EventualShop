using System;
using Application.Abstractions.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Entities.Catalogs;

namespace Application.EventSourcing.EventStore
{
    public interface ICatalogEventStoreRepository : IEventStoreRepository<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid> { }
}