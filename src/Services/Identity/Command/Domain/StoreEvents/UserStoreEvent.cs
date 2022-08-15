using Domain.Abstractions.EventStore;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record UserStoreEvent : StoreEvent<Guid, User>;