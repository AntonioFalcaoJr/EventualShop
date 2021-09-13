using System;
using Application.EventSourcing.Catalogs.EventStore;
using Application.EventSourcing.Catalogs.EventStore.Events;
using Domain.Entities.Catalogs;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.Catalogs.EventStore
{
    public class CatalogEventStoreService : EventStoreService<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid>, ICatalogEventStoreService
    {
        public CatalogEventStoreService(IOptionsMonitor<EventStoreOptions> optionsMonitor, ICatalogEventStoreRepository repository, IBus bus)
            : base(optionsMonitor, repository, bus) { }
    }
}