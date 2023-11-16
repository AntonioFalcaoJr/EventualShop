using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Warehouse;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class IncreaseInventoryAdjustInteractor(IApplicationService service) : IInteractor<Command.IncreaseInventoryAdjust>
{
    public async Task InteractAsync(Command.IncreaseInventoryAdjust command, CancellationToken cancellationToken)
    {
        var inventory = await service.LoadAggregateAsync<Inventory>(command.InventoryId, cancellationToken);
        inventory.Handle(command);
        await service.AppendEventsAsync(inventory, cancellationToken);
    }
}