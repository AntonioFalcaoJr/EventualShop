using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.EventStore
{
    public class OrderEventStoreService : EventStoreService<Order, OrderStoreEvent, OrderSnapshot, Guid>, IOrderEventStoreService
    {
        public OrderEventStoreService(ILogger<OrderEventStoreService> logger, IOptionsMonitor<EventStoreOptions> optionsMonitor, IOrderEventStoreRepository repository, IBus bus)
            : base(logger, optionsMonitor, repository, bus) { }
    }
}