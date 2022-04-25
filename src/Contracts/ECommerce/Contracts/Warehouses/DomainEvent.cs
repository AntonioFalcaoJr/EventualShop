using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Warehouses;

public static class DomainEvent
{
    public record InventoryReceived(Guid ProductId, Models.Product Product, int Quantity) : Message(CorrelationId: ProductId), IEvent;

    public record InventoryAdjustmentIncreased(Guid ProductId, string Reason, int Quantity) : Message(CorrelationId: ProductId), IEvent;

    public record InventoryAdjustmentDecreased(Guid ProductId, string Reason, int Quantity) : Message(CorrelationId: ProductId), IEvent;

    public record InventoryReserved(Guid ProductId, Guid OrderId, string Sku, int Quantity) : Message(CorrelationId: ProductId), IEvent;

    public record StockDepleted(Guid ProductId, string Sku) : Message(CorrelationId: ProductId), IEvent;

    public record InventoryNotReserved(Guid ProductId, Guid CartId, string Sku, int QuantityDesired, int QuantityAvailable) : Message(CorrelationId: ProductId), IEvent;
}