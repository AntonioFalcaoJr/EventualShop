using Application.Abstractions;
using Application.Services;
using Contracts.Services.Warehouse;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class IncreaseInventoryAdjustInteractor : IInteractor<Command.IncreaseInventoryAdjust>
{
    private readonly IApplicationService _applicationService;

    public IncreaseInventoryAdjustInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.IncreaseInventoryAdjust command, CancellationToken cancellationToken)
    {
        var inventory = await _applicationService.LoadAggregateAsync<Inventory>(command.InventoryId, cancellationToken);
        inventory.Handle(command);
        await _applicationService.AppendEventsAsync(inventory, cancellationToken);
    }
}