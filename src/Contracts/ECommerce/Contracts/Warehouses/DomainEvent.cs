using ECommerce.Abstractions;

namespace ECommerce.Contracts.Warehouses;

public static class DomainEvent
{
    public record InventoryReceived(Guid ProductId, string Sku, string Name, string Description, int Quantity) : Message(CorrelationId: ProductId), IEvent;

    public record InventoryAdjusted(Guid ProductId, string Reason, int Quantity) : Message(CorrelationId: ProductId), IEvent;

    public record InventoryReserved(Guid ProductId, Guid OrderId, string Sku, int Quantity) : Message(CorrelationId: ProductId), IEvent;

    public record StockDepleted(Guid ProductId, string Sku) : Message(CorrelationId: ProductId), IEvent;

    public record InventoryNotReserved(Guid ProductId, Guid CartId, string Sku, int QuantityDesired, int QuantityAvailable) : Message(CorrelationId: ProductId), IEvent;
}