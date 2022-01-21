using System;
using Application.Abstractions.Notifications;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.EventStore;

public class UserEventStoreService : EventStoreService<User, UserStoreEvent, UserSnapshot, Guid>, IUserEventStoreService
{
    public UserEventStoreService(IBus bus, IUserEventStoreRepository repository, INotificationContext notificationContext, IOptionsMonitor<EventStoreOptions> optionsMonitor)
        : base(bus, repository, notificationContext, optionsMonitor) { }
}