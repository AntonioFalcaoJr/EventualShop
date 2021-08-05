using System;
using Application.DependencyInjection.Options;
using Application.EventSourcing.Customers.EventStore;
using Application.EventSourcing.Customers.EventStore.Events;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using MassTransit.Mediator;
using Microsoft.Extensions.Options;

namespace Infrastructure.EventSourcing.Customers.EventStore
{
    public class CustomerEventStoreService : EventStoreService<Customer, CustomerStoreEvent, CustomerSnapshot, Guid>, ICustomerEventStoreService
    {
        public CustomerEventStoreService(IOptionsSnapshot<EventStoreOptions> options, ICustomerEventStoreRepository repository, IMediator mediator)
            : base(options, repository, mediator) { }
    }
}