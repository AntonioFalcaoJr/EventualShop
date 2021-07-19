using System;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.EventSourcing.EventStore.Repositories;

namespace Infrastructure.EventSourcing.EventStore.Customers.Repositories
{
    public interface ICustomerEventStoreRepository : IEventStoreRepository<Customer, CustomerStoreEvent, CustomerSnapshot, Guid> { }
}