using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain;

public static class StoreEvents
{
    public record Event : StoreEvent<Payment, Guid>;

    public record Snapshot : Snapshot<Payment, Guid>;
}