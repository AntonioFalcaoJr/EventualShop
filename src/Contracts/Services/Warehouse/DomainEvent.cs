using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class DomainEvent
{
    public record InventoryCreated(Guid Id, Guid OwnerId) : Message(CorrelationId: Id), IEvent;

    public record InventoryItemReceived(Guid Id, Guid InventoryItemId, Dto.Product Product, decimal Cost, int Quantity, string Sku) : Message(CorrelationId: Id), IEvent;

    public record InventoryAdjustmentIncreased(Guid Id, Guid InventoryItemId, string Reason, int Quantity) : Message(CorrelationId: Id), IEvent;

    public record InventoryAdjustmentDecreased(Guid Id, Guid InventoryItemId, string Reason, int Quantity) : Message(CorrelationId: Id), IEvent;

    public record InventoryAdjustmentNotDecreased(Guid Id, Guid InventoryItemId, string Reason, int QuantityDesired, int QuantityAvailable) : Message(CorrelationId: Id), IEvent;

    public record InventoryReserved(Guid Id, Guid CatalogId, Guid CartId, Guid InventoryItemId, string Sku, int Quantity, DateTimeOffset Expiration) : Message(CorrelationId: Id), IEvent;

    public record StockDepleted(Guid Id, Guid InventoryItemId, Dto.Product Product) : Message(CorrelationId: Id), IEvent;

    public record InventoryNotReserved(Guid Id, Guid CartId, Guid InventoryItemId, string Sku, int QuantityDesired, int QuantityAvailable) : Message(CorrelationId: Id), IEvent;

    public record InventoryItemIncreased(Guid Id, Guid InventoryItemId, int Quantity) : Message(CorrelationId: Id), IEvent;

    public record InventoryItemDecreased(Guid Id, Guid InventoryItemId, int Quantity) : Message(CorrelationId: Id), IEvent;
}