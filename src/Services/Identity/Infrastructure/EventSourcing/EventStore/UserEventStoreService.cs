using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates.Users;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.EventStore
{
    public class UserEventStoreService : EventStoreService<User, UserStoreEvent, UserSnapshot, Guid>, IUserEventStoreService
    {
        public UserEventStoreService(IOptionsMonitor<EventStoreOptions> optionsMonitor, IUserEventStoreRepository repository, IBus bus)
            : base(optionsMonitor, repository, bus) { }
    }
}