using Application.Abstractions;
using Application.Services;
using Contracts.Services.Warehouse;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class CreateInventoryInteractor : IInteractor<Command.CreateInventory>
{
    private readonly IApplicationService _applicationService;

    public CreateInventoryInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.CreateInventory command, CancellationToken cancellationToken)
    {
        Inventory inventory = new();
        inventory.Handle(command);
        await _applicationService.AppendEventsAsync(inventory, cancellationToken);
    }
}