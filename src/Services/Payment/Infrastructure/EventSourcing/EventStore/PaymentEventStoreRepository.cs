using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventSourcing.EventStore;

public class PaymentEventStoreRepository : EventStoreRepository<Payment, PaymentStoreEvent, PaymentSnapshot, Guid>, IPaymentEventStoreRepository
{
    public PaymentEventStoreRepository(DbContext dbContext) 
        : base(dbContext) { }
}