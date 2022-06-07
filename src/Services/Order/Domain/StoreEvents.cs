using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain;

public static class StoreEvents
{
    public record Event : StoreEvent<Order, Guid>;

    public record Snapshot : Snapshot<Order, Guid>;
}