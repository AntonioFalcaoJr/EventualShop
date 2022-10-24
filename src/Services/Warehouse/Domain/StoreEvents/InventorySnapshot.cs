using Domain.Abstractions.StoreEvents;
using Domain.Aggregates;

namespace Domain.StoreEvents;

public record InventorySnapshot : Snapshot<Inventory>;