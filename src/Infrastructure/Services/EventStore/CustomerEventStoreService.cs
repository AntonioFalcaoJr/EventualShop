using System;
using Application.Interfaces;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.Services.EventStore;
using Infrastructure.DependencyInjection.Options;
using Infrastructure.EventSourcing.EventStore.Customers;
using Infrastructure.EventSourcing.EventStore.Customers.Repositories;
using MassTransit.Mediator;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.EventStore
{
    public class CustomerEventStoreService : EventStoreService<Customer, CustomerStoreEvent, CustomerSnapshot, Guid>, ICustomerEventStoreService
    {
        public CustomerEventStoreService(IOptions<EventStoreOptions> options, ICustomerEventStoreRepository repository, IMediator mediator)
            : base(options, repository, mediator) { }
    }
}