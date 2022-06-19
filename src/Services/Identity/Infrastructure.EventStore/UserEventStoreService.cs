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

public class UserEventStoreService : EventStoreService<User, UserStoreEvent, UserSnapshot, Guid>, IUserEventStoreService
{
    public UserEventStoreService(IPublishEndpoint publishEndpoint, IUserEventStoreRepository repository, INotificationContext notificationContext, IOptionsMonitor<EventStoreOptions> optionsMonitor, IUnitOfWork unitOfWork)
        : base(publishEndpoint, repository, notificationContext, optionsMonitor, unitOfWork) { }
}