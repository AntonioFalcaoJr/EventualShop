using Application.Abstractions.Notifications;
using Application.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.DependencyInjection.Options;
using Infrastructure.EventStore.UnitsOfWork;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore;

public class WarehouseEventStoreService : EventStoreService<Inventory, InventoryStoreEvent, InventorySnapshot, Guid>, IWarehouseEventStoreService
{
    public WarehouseEventStoreService(IPublishEndpoint publishEndpoint, IWarehouseEventStoreRepository repository, INotificationContext notificationContext, IOptionsMonitor<EventStoreOptions> optionsMonitor, IUnitOfWork unitOfWork)
        : base(publishEndpoint, repository, notificationContext, optionsMonitor, unitOfWork) { }
}