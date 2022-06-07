using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain;

public static class StoreEvents
{
    public record Event : StoreEvent<Account, Guid>;

    public record Snapshot : Snapshot<Account, Guid>;
}