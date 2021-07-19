using System;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.EventSourcing.EventStore.Repositories;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore.Customers.Repositories
{
    public class CustomerEventStoreRepository : EventStoreRepository<Customer, CustomerStoreEvent, CustomerSnapshot, Guid>, ICustomerEventStoreRepository
    {
        public CustomerEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
    }
}