using System;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Entities.Customers;

namespace Application.EventSourcing.Customers.EventStore.Events
{
    public record CustomerStoreEvent : StoreEvent<Customer, Guid>;
}