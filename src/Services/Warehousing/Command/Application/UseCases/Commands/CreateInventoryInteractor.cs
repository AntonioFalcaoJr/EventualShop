using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Warehouse;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class CreateInventoryInteractor(IApplicationService service) : IInteractor<Command.CreateInventory>
{
    public async Task InteractAsync(Command.CreateInventory command, CancellationToken cancellationToken)
    {
        Inventory inventory = new();
        inventory.Handle(command);
        await service.AppendEventsAsync(inventory, cancellationToken);
    }
}