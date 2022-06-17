using Application.Abstractions.EventStore;
using Domain.Aggregates;
using Domain.StoreEvents;

namespace Application.EventStore;

public interface IPaymentEventStoreRepository : IEventStoreRepository<Payment, PaymentStoreEvent, PaymentSnapshot, Guid> { }