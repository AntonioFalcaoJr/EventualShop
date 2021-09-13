using System;
using Application.Abstractions.EventSourcing.EventStore;
using Domain.Entities.Catalogs;

namespace Application.EventSourcing.Catalogs.EventStore
{
    public interface ICatalogEventStoreService : IEventStoreService<Catalog, Guid> { }
}