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

public class ShoppingCartEventStoreService : EventStoreService<Cart, ShoppingCartStoreEvent, ShoppingCartSnapshot, Guid>, IShoppingCartEventStoreService
{
    public ShoppingCartEventStoreService(ILogger<ShoppingCartEventStoreService> logger, IOptionsMonitor<EventStoreOptions> optionsMonitor, IShoppingCartEventStoreRepository repository, IBus bus)
        : base(logger, optionsMonitor, repository, bus) { }
}