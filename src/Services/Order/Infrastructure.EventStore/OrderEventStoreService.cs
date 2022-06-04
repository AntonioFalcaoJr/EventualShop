using Application.Abstractions.Notifications;
using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventStore;

public class OrderEventStoreService : EventStoreService<Order, StoreEvents.Event, StoreEvents.Snapshot, Guid>, IOrderEventStoreService
{
    public OrderEventStoreService(IPublishEndpoint publishEndpoint, IOrderEventStoreRepository repository, INotificationContext notificationContext, IOptionsMonitor<EventStoreOptions> optionsMonitor)
        : base(publishEndpoint, repository, notificationContext, optionsMonitor) { }
}