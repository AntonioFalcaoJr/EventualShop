﻿using Application.Abstractions.EventStore;
using Application.EventStore.Events;
using Domain.Aggregates;

namespace Application.EventStore;

public interface IWarehouseEventStoreRepository : IEventStoreRepository<Inventory, WarehouseStoreEvent, WarehouseSnapshot, Guid> { }