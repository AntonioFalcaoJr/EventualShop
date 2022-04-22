using Application.Abstractions.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore.Events;

public record ShoppingCartSnapshot : Snapshot<ShoppingCart, Guid>;