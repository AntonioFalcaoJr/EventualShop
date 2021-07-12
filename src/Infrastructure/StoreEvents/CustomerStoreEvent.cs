using System;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.StoreEvents;

namespace Infrastructure.StoreEvents
{
    public class CustomerStoreEvent : StoreEvent<Customer, Guid> { }
}