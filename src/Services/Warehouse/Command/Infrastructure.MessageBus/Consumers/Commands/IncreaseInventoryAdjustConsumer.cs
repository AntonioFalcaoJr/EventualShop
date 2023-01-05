using Application.Abstractions;
using Contracts.Services.Warehouse;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class IncreaseInventoryAdjustConsumer : Consumer<Command.IncreaseInventoryAdjust>
{
    public IncreaseInventoryAdjustConsumer(IInteractor<Command.IncreaseInventoryAdjust> interactor)
        : base(interactor) { }
}