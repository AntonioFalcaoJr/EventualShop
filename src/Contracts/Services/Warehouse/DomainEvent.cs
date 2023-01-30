using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class DomainEvent
{
    public record InventoryCreated(Guid InventoryId, Guid OwnerId, long Version) : Message, IDomainEvent;

    public record InventoryItemReceived(Guid InventoryId, Guid ItemId, Dto.Product Product, decimal Cost, int Quantity, string Sku, long Version) : Message, IDomainEvent;

    public record InventoryAdjustmentIncreased(Guid InventoryId, Guid ItemId, string Reason, int Quantity, long Version) : Message, IDomainEvent;

    public record InventoryAdjustmentDecreased(Guid InventoryId, Guid ItemId, string Reason, int Quantity, long Version) : Message, IDomainEvent;

    public record InventoryAdjustmentNotDecreased(Guid InventoryId, Guid ItemId, string Reason, int QuantityDesired, int QuantityAvailable, long Version) : Message, IDomainEvent;

    public record InventoryReserved(Guid InventoryId, Guid ItemId, Guid CatalogId, Guid CartId, Dto.Product Product, int Quantity, DateTimeOffset Expiration, long Version) : Message, IDomainEvent;

    public record StockDepleted(Guid InventoryId, Guid ItemId, Dto.Product Product, long Version) : Message, IDomainEvent;

    public record InventoryNotReserved(Guid InventoryId, Guid ItemId, Guid CartId, int QuantityDesired, int QuantityAvailable, long Version) : Message, IDomainEvent;

    public record InventoryItemIncreased(Guid InventoryId, Guid ItemId, int Quantity, long Version) : Message, IDomainEvent;

    public record InventoryItemDecreased(Guid InventoryId, Guid ItemId, int Quantity, long Version) : Message, IDomainEvent;
}