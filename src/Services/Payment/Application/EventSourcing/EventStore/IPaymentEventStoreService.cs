using Application.Abstractions.EventSourcing.EventStore;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface IPaymentEventStoreService : IEventStoreService<Payment, Guid> { }