using System;
using Domain.Entities.Customers;

namespace Application.Interfaces
{
    public interface ICustomerEventStoreService : IEventStoreService<Customer, Guid> { }
}