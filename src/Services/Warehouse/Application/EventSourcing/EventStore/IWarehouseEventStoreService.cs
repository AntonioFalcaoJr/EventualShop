using System;
using Application.Abstractions.EventSourcing.EventStore;
using Domain.Aggregates;

namespace Application.EventSourcing.EventStore;

public interface IWarehouseEventStoreService : IEventStoreService<Product, Guid> { }