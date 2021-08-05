using System;
using Application.EventSourcing.Customers.EventStore;
using Application.EventSourcing.Customers.EventStore.Events;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.Customers.EventStore.Contexts;

namespace Infrastructure.EventSourcing.Customers.EventStore
{
    public class CustomerEventStoreRepository : EventStoreRepository<Customer, CustomerStoreEvent, CustomerSnapshot, Guid>, ICustomerEventStoreRepository
    {
        public CustomerEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
    }
}