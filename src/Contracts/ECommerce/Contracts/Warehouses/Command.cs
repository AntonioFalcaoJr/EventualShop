using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Warehouses;

public static class Command
{
    public record ReceiveInventoryItem(Models.Product Product, int Quantity) : Message, ICommand;

    public record IncreaseInventoryAdjust(Guid ProductId, int Quantity, string Reason) : Message(CorrelationId: ProductId), ICommand;

    public record DecreaseInventoryAdjust(Guid ProductId, int Quantity, string Reason) : Message(CorrelationId: ProductId), ICommand;

    public record ReserveInventory(Guid ProductId, Guid CartId, string Sku, int Quantity) : Message(CorrelationId: ProductId), ICommand;
}