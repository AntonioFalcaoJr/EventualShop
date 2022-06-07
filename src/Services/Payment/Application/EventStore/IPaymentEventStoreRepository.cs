using Application.Abstractions.EventStore;
using Domain;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IPaymentEventStoreRepository : IEventStoreRepository<Payment, StoreEvents.Event, StoreEvents.Snapshot, Guid> { }