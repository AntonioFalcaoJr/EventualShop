using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain;

public static class StoreEvents
{
    public record Event : StoreEvent<Catalog, Guid>;

    public record Snapshot : Snapshot<Catalog, Guid>;
}