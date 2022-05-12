using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class Command
{
    public record ReceiveInventoryItem(Guid InventoryId, Dto.Product Product, int Quantity) : Message, ICommand;

    public record IncreaseInventoryAdjust(Guid InventoryId, Guid InventoryItemId, int Quantity, string Reason) : Message(CorrelationId: InventoryId), ICommand;

    public record DecreaseInventoryAdjust(Guid InventoryId, Guid InventoryItemId, int Quantity, string Reason) : Message(CorrelationId: InventoryId), ICommand;

    public record ReserveInventoryItem(Guid CartId, Guid CatalogId, Dto.Product Product, int Quantity) : Message(CorrelationId: CartId), ICommand;

    public record CreateInventory(Guid OwnerId) : Message(CorrelationId: OwnerId), ICommand;
}