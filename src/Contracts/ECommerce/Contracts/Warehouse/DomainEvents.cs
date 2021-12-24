using System;
using ECommerce.Abstractions.Events;

namespace ECommerce.Contracts.Warehouse;

public static class DomainEvents
{
    public record InventoryItemReceived(Guid ProductId, string Sku, string Name, string Description, int Quantity) : Event;

    public record InventoryAdjusted(Guid ProductId, string Sku, int Quantity) : Event;

    public record InventoryReserved(Guid ProductId, Guid OrderId, string Sku, int Quantity) : Event;

    public record StockDepleted(Guid ProductId, string Sku) : Event;

    public record InventoryNotReserved(Guid ProductId, Guid CartId, string Sku, int QuantityDesired, int QuantityAvailable) : Event;
}