using System;
using Application.Abstractions.EventSourcing.Services.EventStore.Events;
using Domain.Entities.Customers;

namespace Application.EventSourcing.Customers.EventStore.Events
{
    public record CustomerSnapshot : Snapshot<Customer, Guid>;
}