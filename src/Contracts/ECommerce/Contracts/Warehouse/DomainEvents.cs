using ECommerce.Abstractions.Messages.Events;

namespace ECommerce.Contracts.Warehouse;

public static class DomainEvents
{
    public record InventoryItemReceived(Guid ProductId, string Sku, string Name, string Description, int Quantity) : Event(CorrelationId: ProductId);

    public record InventoryAdjusted(Guid ProductId, string Reason, int Quantity) : Event(CorrelationId: ProductId);

    public record InventoryReserved(Guid ProductId, Guid OrderId, string Sku, int Quantity) : Event(CorrelationId: ProductId);

    public record StockDepleted(Guid ProductId, string Sku) : Event(CorrelationId: ProductId);

    public record InventoryNotReserved(Guid ProductId, Guid CartId, string Sku, int QuantityDesired, int QuantityAvailable) : Event(CorrelationId: ProductId);
}