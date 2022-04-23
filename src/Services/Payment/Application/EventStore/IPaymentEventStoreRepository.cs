using Application.Abstractions.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IPaymentEventStoreRepository : IEventStoreRepository<Payment, PaymentStoreEvent, PaymentSnapshot, Guid> { }