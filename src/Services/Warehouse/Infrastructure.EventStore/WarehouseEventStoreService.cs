using Application.Abstractions.Notifications;
using Application.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore;

public class WarehouseEventStoreService : EventStoreService<Inventory, WarehouseStoreEvent, WarehouseSnapshot, Guid>, IWarehouseEventStoreService
{
    public WarehouseEventStoreService(IPublishEndpoint publishEndpoint, IWarehouseEventStoreRepository repository, INotificationContext notificationContext, IOptionsMonitor<EventStoreOptions> optionsMonitor)
        : base(publishEndpoint, repository, notificationContext, optionsMonitor) { }
}