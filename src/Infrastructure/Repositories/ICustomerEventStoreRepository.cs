using System;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.Repositories;
using Infrastructure.StoreEvents;

namespace Infrastructure.Repositories
{
    public interface ICustomerEventStoreRepository : IEventStoreRepository<Customer, CustomerStoreEvent, Guid> { }
}