using System;
using Application.Abstractions.Services;
using Domain.Entities.Customers;

namespace Application.Interfaces.Customers
{
    public interface ICustomerEventStoreService : IEventStoreService<Customer, Guid> { }
}