using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class DomainEvent
{
    public record InventoryCreated(Guid InventoryId, Guid OwnerId) : Message(CorrelationId: OwnerId), IEvent;

    public record InventoryItemReceived(Guid InventoryId, Guid InventoryItemId, Dto.Product Product, decimal Cost, int Quantity, string Sku) : Message(CorrelationId: InventoryItemId), IEvent;

    public record InventoryAdjustmentIncreased(Guid InventoryId, Guid InventoryItemId, string Reason, int Quantity) : Message(CorrelationId: InventoryId), IEvent;

    public record InventoryAdjustmentDecreased(Guid InventoryId, Guid InventoryItemId, string Reason, int Quantity) : Message(CorrelationId: InventoryId), IEvent;

    public record InventoryAdjustmentNotDecreased(Guid InventoryId, Guid InventoryItemId, string Reason, int QuantityDesired, int QuantityAvailable) : Message(CorrelationId: InventoryId), IEvent;

    public record InventoryReserved(Guid InventoryId, Guid CatalogId, Guid CartId, Guid InventoryItemId, string Sku, int Quantity, DateTimeOffset Expiration) : Message(CorrelationId: CartId), IEvent;

    public record StockDepleted(Guid InventoryItemId, Dto.Product Product) : Message(CorrelationId: InventoryItemId), IEvent;

    public record InventoryNotReserved(Guid InventoryId, Guid CartId, Guid InventoryItemId, string Sku, int QuantityDesired, int QuantityAvailable) : Message(CorrelationId: CartId), IEvent;

    public record InventoryItemIncreased(Guid InventoryId, Guid InventoryItemId, int Quantity) : Message(CorrelationId: InventoryId), IEvent;
}