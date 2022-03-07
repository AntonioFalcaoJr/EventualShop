using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Infrastructure.EventSourcing.EventStore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventSourcing.EventStore;

public class OrderEventStoreRepository : EventStoreRepository<Order, OrderStoreEvent, OrderSnapshot, Guid>, IOrderEventStoreRepository
{
    public OrderEventStoreRepository(DbContext dbContext) 
        : base(dbContext) { }
}