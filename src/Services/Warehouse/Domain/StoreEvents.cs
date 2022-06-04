using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain;

public static class StoreEvents
{
    public record Event : StoreEvent<Inventory, Guid>;

    public record Snapshot : Snapshot<Inventory, Guid>;
}