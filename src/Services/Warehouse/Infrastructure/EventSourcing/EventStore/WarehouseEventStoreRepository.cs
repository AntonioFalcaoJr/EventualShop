using System;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.EventStore.Events;
using Domain.Aggregates;
using Infrastructure.Abstractions.EventSourcing.EventStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EventSourcing.EventStore;

public class WarehouseEventStoreRepository : EventStoreRepository<InventoryItem, WarehouseStoreEvent, WarehouseSnapshot, Guid>, IWarehouseEventStoreRepository
{
    public WarehouseEventStoreRepository(DbContext dbContext) 
        : base(dbContext) { }
}