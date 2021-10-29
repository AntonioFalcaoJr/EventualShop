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

public class AccountEventStoreService : EventStoreService<Account, AccountStoreEvent, AccountSnapshot, Guid>, IAccountEventStoreService
{
    public AccountEventStoreService(ILogger<AccountEventStoreService> logger, IOptionsMonitor<EventStoreOptions> optionsMonitor, IAccountEventStoreRepository repository, IBus bus)
        : base(logger, optionsMonitor, repository, bus) { }
}