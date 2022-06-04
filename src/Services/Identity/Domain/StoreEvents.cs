using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain;

public static class StoreEvents
{
    public record Event : StoreEvent<User, Guid>;

    public record Snapshot : Snapshot<User, Guid>;
}