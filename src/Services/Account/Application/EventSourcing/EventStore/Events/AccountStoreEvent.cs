using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore.Events;

public record AccountStoreEvent : StoreEvent<Account, Guid>;