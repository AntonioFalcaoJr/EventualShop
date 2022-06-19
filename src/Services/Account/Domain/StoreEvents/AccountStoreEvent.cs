using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record AccountStoreEvent : StoreEvent<Guid, Account>;
