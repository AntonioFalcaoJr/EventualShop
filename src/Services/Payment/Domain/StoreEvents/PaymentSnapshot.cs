using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record PaymentSnapshot : Snapshot<Guid, Payment>;