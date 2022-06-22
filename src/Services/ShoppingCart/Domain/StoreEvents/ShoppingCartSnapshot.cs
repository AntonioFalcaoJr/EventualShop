using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record ShoppingCartSnapshot : Snapshot<Guid, ShoppingCart>;