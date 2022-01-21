using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore;

public class CatalogEventStoreRepository : EventStoreRepository<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid>, ICatalogEventStoreRepository
{
    public CatalogEventStoreRepository(EventStoreDbContext dbContext) 
        : base(dbContext) { }
}