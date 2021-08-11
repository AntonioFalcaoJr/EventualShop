using System;
using Application.EventSourcing.Customers.EventStore;
using Application.EventSourcing.Customers.EventStore.Events;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.DependencyInjection.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.Customers.EventStore
{
    public class CustomerEventStoreService : EventStoreService<Customer, CustomerStoreEvent, CustomerSnapshot, Guid>, ICustomerEventStoreService
    {
        public CustomerEventStoreService(IOptionsMonitor<EventStoreOptions> optionsMonitor, ICustomerEventStoreRepository repository, IBus bus)
            : base(optionsMonitor, repository, bus) { }
    }
}