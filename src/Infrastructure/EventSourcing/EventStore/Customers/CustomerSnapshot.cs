using System;
using Domain.Entities.Customers;
using Infrastructure.Abstractions.EventSourcing.EventStore;

namespace Infrastructure.EventSourcing.EventStore.Customers
{
    public record CustomerSnapshot : Snapshot<Customer, Guid>;
}