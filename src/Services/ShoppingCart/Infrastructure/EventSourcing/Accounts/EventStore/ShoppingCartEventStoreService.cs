using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Entities.ShoppingCarts;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.Accounts.EventStore
{
    public class ShoppingCartEventStoreService : EventStoreService<ShoppingCart, AccountStoreEvent, AccountSnapshot, Guid>, IShoppingCartEventStoreService
    {
        public ShoppingCartEventStoreService(IOptionsMonitor<EventStoreOptions> optionsMonitor, IAccountEventStoreRepository repository, IBus bus)
            : base(optionsMonitor, repository, bus) { }
    }
}