using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouses;

public static class Command
{
    public record ReceiveInventoryItem(Dto.Product Product, int Quantity) : Message, ICommand;

    public record IncreaseInventoryAdjust(Guid ProductId, int Quantity, string Reason) : Message(CorrelationId: ProductId), ICommand;

    public record DecreaseInventoryAdjust(Guid ProductId, int Quantity, string Reason) : Message(CorrelationId: ProductId), ICommand;

    public record ReserveInventory(Guid ProductId, Guid CartId, string Sku, int Quantity) : Message(CorrelationId: ProductId), ICommand;
}