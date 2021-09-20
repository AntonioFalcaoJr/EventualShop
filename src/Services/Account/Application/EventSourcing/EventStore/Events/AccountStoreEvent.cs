using System;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Aggregates.Accounts;

namespace Application.EventSourcing.EventStore.Events
{
    public record AccountStoreEvent : StoreEvent<Account, Guid>;
}