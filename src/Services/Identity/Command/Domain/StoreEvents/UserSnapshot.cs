using Domain.Abstractions.EventStore;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record UserSnapshot : Snapshot<Guid, User>;