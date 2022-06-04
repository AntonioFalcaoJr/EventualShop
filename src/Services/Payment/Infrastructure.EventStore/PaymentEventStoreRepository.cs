using Application.EventStore;
using Domain;
using Domain.Aggregates;
using Infrastructure.EventStore.Abstractions;
using Infrastructure.EventStore.Contexts;

namespace Infrastructure.EventStore;

public class PaymentEventStoreRepository : EventStoreRepository<Payment, StoreEvents.Event, StoreEvents.Snapshot, Guid>, IPaymentEventStoreRepository
{
    public PaymentEventStoreRepository(EventStoreDbContext dbContext)
        : base(dbContext) { }
}