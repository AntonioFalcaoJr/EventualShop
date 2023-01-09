using Application.Abstractions;
using Application.Services;
using Contracts.Services.Warehouse;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class DecreaseInventoryAdjustInteractor : IInteractor<Command.DecreaseInventoryAdjust>
{
    private readonly IApplicationService _applicationService;

    public DecreaseInventoryAdjustInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.DecreaseInventoryAdjust command, CancellationToken cancellationToken)
    {
        var inventory = await _applicationService.LoadAggregateAsync<Inventory>(command.InventoryId, cancellationToken);
        inventory.Handle(command);
        await _applicationService.AppendEventsAsync(inventory, cancellationToken);
    }
}