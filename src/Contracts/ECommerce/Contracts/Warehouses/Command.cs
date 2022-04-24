using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Commands;

namespace ECommerce.Contracts.Warehouses;

public static class Command
{
    public record ReceiveInventoryItem(string Sku, string Name, string Description, int Quantity) : Message, ICommand;

    public record AdjustInventory(Guid ProductId, int Quantity, string Reason) : Message(CorrelationId: ProductId), ICommand;

    public record ReserveInventory(Guid ProductId, Guid CartId, string Sku, int Quantity) : Message(CorrelationId: ProductId), ICommand;
}