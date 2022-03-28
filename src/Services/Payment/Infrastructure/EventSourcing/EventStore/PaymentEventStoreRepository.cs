using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;

namespace Infrastructure.EventSourcing.EventStore;

public class PaymentEventStoreRepository : EventStoreRepository<Payment, PaymentStoreEvent, PaymentSnapshot, Guid>, IPaymentEventStoreRepository
{
    public PaymentEventStoreRepository(EventStoreDbContext dbContext) 
        : base(dbContext) { }
}