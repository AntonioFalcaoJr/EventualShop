using ECommerce.Abstractions.Commands;

namespace ECommerce.Contracts.Warehouse;

public static class Commands
{
    public record ReceiveProduct(string Sku, string Name, string Description, int Quantity) : Command;

    public record AdjustInventory(Guid ProductId, string Sku, int Quantity) : Command;
}