using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.EventStore;

public class CatalogEventStoreService : EventStoreService<Catalog, CatalogStoreEvent, CatalogSnapshot, Guid>, ICatalogEventStoreService
{
    public CatalogEventStoreService(ILogger<CatalogEventStoreService> logger, IOptionsMonitor<EventStoreOptions> optionsMonitor, ICatalogEventStoreRepository repository, IBus bus)
        : base(logger, optionsMonitor, repository, bus) { }
}