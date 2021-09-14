using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Entities.Catalogs;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.Catalogs.EventStore.Contexts;

namespace Infrastructure.EventSourcing.Catalogs.EventStore
{
    public class CatalogEventStoreRepository : EventStoreRepository<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid>, ICatalogEventStoreRepository
    {
        public CatalogEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
    }
}