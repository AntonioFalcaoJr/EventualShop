using Application.Abstractions;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Events;

public interface IProjectInventoryDetailsWhenChangedInteractor : IInteractor<DomainEvent.InventoryCreated> { }

public class ProjectInventoryDetailsWhenChangedInteractor : IProjectInventoryDetailsWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.Inventory> _projectionGateway;

    public ProjectInventoryDetailsWhenChangedInteractor(IProjectionGateway<Projection.Inventory> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.InventoryCreated @event, CancellationToken cancellationToken)
    {
        Projection.Inventory card = new(
            @event.InventoryId,
            @event.OwnerId,
            false);

        await _projectionGateway.UpsertAsync(card, cancellationToken);
    }
}