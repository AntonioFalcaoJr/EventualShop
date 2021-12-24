using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.EventStore;

public class WarehouseEventStoreService : EventStoreService<InventoryItem, WarehouseStoreEvent, WarehouseSnapshot, Guid>, IWarehouseEventStoreService
{
    public WarehouseEventStoreService(ILogger<WarehouseEventStoreService> logger, IOptionsMonitor<EventStoreOptions> optionsMonitor, IWarehouseEventStoreRepository repository, IBus bus)
        : base(logger, optionsMonitor, repository, bus) { }
}