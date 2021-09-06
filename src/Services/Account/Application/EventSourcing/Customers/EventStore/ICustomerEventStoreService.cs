using System;
using Application.Abstractions.EventSourcing.EventStore;
using Domain.Entities.Customers;

namespace Application.EventSourcing.Customers.EventStore
{
    public interface ICustomerEventStoreService : IEventStoreService<Customer, Guid> { }
}