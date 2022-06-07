using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain;

public static class StoreEvents
{
    public record Event : StoreEvent<ShoppingCart, Guid>;

    public record Snapshot : Snapshot<ShoppingCart, Guid>;
}