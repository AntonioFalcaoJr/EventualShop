using System;
using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Aggregates.Users;

namespace Application.EventSourcing.EventStore.Events
{
    public record UserStoreEvent : StoreEvent<User, Guid>;
}