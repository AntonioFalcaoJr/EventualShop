using Application.Abstractions;
using Contracts.Services.Warehouse;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class DecreaseInventoryAdjustConsumer : Consumer<Command.DecreaseInventoryAdjust>
{
    public DecreaseInventoryAdjustConsumer(IInteractor<Command.DecreaseInventoryAdjust> interactor)
        : base(interactor) { }
}