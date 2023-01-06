using Application.Abstractions;
using Contracts.Services.Warehouse;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class ReceiveInventoryItemConsumer : Consumer<Command.ReceiveInventoryItem>
{
    public ReceiveInventoryItemConsumer(IInteractor<Command.ReceiveInventoryItem> interactor)
        : base(interactor) { }
}