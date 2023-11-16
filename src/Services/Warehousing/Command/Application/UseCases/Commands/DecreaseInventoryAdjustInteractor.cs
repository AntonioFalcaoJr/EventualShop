using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Warehouse;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class DecreaseInventoryAdjustInteractor(IApplicationService service) : IInteractor<Command.DecreaseInventoryAdjust>
{
    public async Task InteractAsync(Command.DecreaseInventoryAdjust command, CancellationToken cancellationToken)
    {
        var inventory = await service.LoadAggregateAsync<Inventory>(command.InventoryId, cancellationToken);
        inventory.Handle(command);
        await service.AppendEventsAsync(inventory, cancellationToken);
    }
}