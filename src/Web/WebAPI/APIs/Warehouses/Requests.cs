using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Warehouse;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Warehouses.Validators;

namespace WebAPI.APIs.Warehouses;

public static class Requests
{
    public record CreateInventory(IBus Bus, Guid OwnerId, CancellationToken CancellationToken)
        : Validatable<CreateInventoryValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.CreateInventory(Guid.NewGuid(), OwnerId);
    }

    public record ReceiveInventoryItem(IBus Bus, Guid InventoryId, Dto.Product Product, decimal Cost, int Quantity, CancellationToken CancellationToken)
        : Validatable<ReceiveInventoryItemValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ReceiveInventoryItem(InventoryId, Product, Cost, Quantity);
    }

    public record IncreaseInventoryAdjust(IBus Bus, Guid InventoryId, Guid ItemId, int Quantity, string Reason, CancellationToken CancellationToken)
        : Validatable<IncreaseInventoryAdjustValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.IncreaseInventoryAdjust(InventoryId, ItemId, Quantity, Reason);
    }

    public record DecreaseInventoryAdjust(IBus Bus, Guid InventoryId, Guid ItemId, int Quantity, string Reason, CancellationToken CancellationToken)
        : Validatable<DecreaseInventoryAdjustValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.DecreaseInventoryAdjust(InventoryId, ItemId, Quantity, Reason);
    }
}