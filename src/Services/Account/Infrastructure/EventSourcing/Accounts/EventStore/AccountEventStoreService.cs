using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates.Accounts;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.Accounts.EventStore
{
    public class AccountEventStoreService : EventStoreService<Account, AccountStoreEvent, AccountSnapshot, Guid>, IAccountEventStoreService
    {
        public AccountEventStoreService(IOptionsMonitor<EventStoreOptions> optionsMonitor, IAccountEventStoreRepository repository, IBus bus)
            : base(optionsMonitor, repository, bus) { }
    }
}