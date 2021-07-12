using System;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.Repositories;
using Infrastructure.Contexts;
using Infrastructure.StoreEvents;

namespace Infrastructure.Repositories
{
    public class CustomerEventStoreRepository : EventStoreRepository<Customer, CustomerStoreEvent, Guid>, ICustomerEventStoreRepository
    {
        public CustomerEventStoreRepository(EventStoreDbContext dbContext) : base(dbContext) { }
    }
}