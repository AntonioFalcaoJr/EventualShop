using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class Command
{
    public record ReceiveInventoryItem(Guid Id, Dto.Product Product, decimal Cost, int Quantity) : Message, ICommand;

    public record IncreaseInventoryAdjust(Guid Id, Guid ItemId, int Quantity, string Reason) : Message(CorrelationId: Id), ICommand;

    public record DecreaseInventoryAdjust(Guid Id, Guid ItemId, int Quantity, string Reason) : Message(CorrelationId: Id), ICommand;

    public record ReserveInventoryItem(Guid Id, Guid CatalogId, Guid CartId, Dto.Product Product, int Quantity) : Message(CorrelationId: Id), ICommand;

    public record CreateInventory(Guid Id, Guid OwnerId) : Message(CorrelationId: Id), ICommand;
}