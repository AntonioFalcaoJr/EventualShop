using System;
using ECommerce.Abstractions.Events;

namespace ECommerce.Contracts.Warehouse;

public static class DomainEvents
{
    public record ProductReceived(Guid ProductId, string Sku, string Name, string Description, int Quantity) : Event;

    public record InventoryAdjusted(Guid ProductId, string Sku, int Quantity) : Event;
}