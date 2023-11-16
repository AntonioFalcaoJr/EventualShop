using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Boundaries.Warehouse;

public static class DomainEvent
{
    public record InventoryCreated(Guid InventoryId, Guid OwnerId, string Version) : Message, IDomainEvent;

    public record InventoryItemReceived(Guid InventoryId, Guid ItemId, Dto.Product Product, decimal Cost, int Quantity, string Sku, string Version) : Message, IDomainEvent;

    public record InventoryAdjustmentIncreased(Guid InventoryId, Guid ItemId, string Reason, int Quantity, string Version) : Message, IDomainEvent;

    public record InventoryAdjustmentDecreased(Guid InventoryId, Guid ItemId, string Reason, int Quantity, string Version) : Message, IDomainEvent;

    public record InventoryAdjustmentNotDecreased(Guid InventoryId, Guid ItemId, string Reason, int QuantityDesired, int QuantityAvailable, string Version) : Message, IDomainEvent;

    public record InventoryReserved(Guid InventoryId, Guid ItemId, Guid CatalogId, Guid CartId, Dto.Product Product, int Quantity, DateTimeOffset Expiration, string Version) : Message, IDomainEvent;

    public record StockDepleted(Guid InventoryId, Guid ItemId, Dto.Product Product, string Version) : Message, IDomainEvent;

    public record InventoryNotReserved(Guid InventoryId, Guid ItemId, Guid CartId, int QuantityDesired, int QuantityAvailable, string Version) : Message, IDomainEvent;

    public record InventoryItemIncreased(Guid InventoryId, Guid ItemId, int Quantity, string Version) : Message, IDomainEvent;

    public record InventoryItemDecreased(Guid InventoryId, Guid ItemId, int Quantity, string Version) : Message, IDomainEvent;
}