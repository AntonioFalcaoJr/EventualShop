using Application.Abstractions.EventSourcing.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore.Events;

public record ShoppingCartStoreEvent : StoreEvent<ShoppingCart, Guid>;