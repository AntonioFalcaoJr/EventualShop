using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class DomainEvent
{
    public record InventoryCreated(Guid InventoryId, Guid OwnerId) : Message, IEvent;

    public record InventoryItemReceived(Guid InventoryId, Guid ItemId, Dto.Product Product, decimal Cost, int Quantity, string Sku) : Message, IEvent;

    public record InventoryAdjustmentIncreased(Guid InventoryId, Guid ItemId, string Reason, int Quantity) : Message, IEvent;

    public record InventoryAdjustmentDecreased(Guid InventoryId, Guid ItemId, string Reason, int Quantity) : Message, IEvent;

    public record InventoryAdjustmentNotDecreased(Guid InventoryId, Guid ItemId, string Reason, int QuantityDesired, int QuantityAvailable) : Message, IEvent;

    public record InventoryReserved(Guid InventoryId, Guid ItemId, Guid CatalogId, Guid CartId, Dto.Product Product, int Quantity, DateTimeOffset Expiration) : Message, IEvent;

    public record StockDepleted(Guid InventoryId, Guid ItemId, Dto.Product Product) : Message, IEvent;

    public record InventoryNotReserved(Guid InventoryId, Guid ItemId, Guid CartId, int QuantityDesired, int QuantityAvailable) : Message, IEvent;

    public record InventoryItemIncreased(Guid InventoryId, Guid ItemId, int Quantity) : Message, IEvent;

    public record InventoryItemDecreased(Guid InventoryId, Guid ItemId, int Quantity) : Message, IEvent;
}