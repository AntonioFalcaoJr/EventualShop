using Application.Abstractions;
using Contracts.Services.Warehouse;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class CreateInventoryConsumer : Consumer<Command.CreateInventory>
{
    public CreateInventoryConsumer(IInteractor<Command.CreateInventory> interactor)
        : base(interactor) { }
}