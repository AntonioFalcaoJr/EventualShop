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

public class OrderEventStoreService : EventStoreService<Order, OrderStoreEvent, OrderSnapshot>, IOrderEventStoreService
{
    public OrderEventStoreService(IPublishEndpoint publishEndpoint, IOrderEventStoreRepository repository, INotificationContext notificationContext, IOptionsSnapshot<EventStoreOptions> options, IUnitOfWork unitOfWork)
        : base(publishEndpoint, repository, notificationContext, options, unitOfWork) { }
}