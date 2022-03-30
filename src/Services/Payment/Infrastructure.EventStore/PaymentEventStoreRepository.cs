using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class PaymentEventStoreRepository : EventStoreRepository<Payment, PaymentStoreEvent, PaymentSnapshot, Guid>, IPaymentEventStoreRepository
{
    public PaymentEventStoreRepository(EventStoreDbContext dbContext) 
        : base(dbContext) { }
}