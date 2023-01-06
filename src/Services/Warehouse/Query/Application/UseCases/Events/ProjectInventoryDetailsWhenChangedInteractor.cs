using Application.Abstractions;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Events;

public interface IProjectInventoryDetailsWhenChangedInteractor : IInteractor<DomainEvent.InventoryCreated> { }

public class ProjectInventoryDetailsWhenChangedInteractor : IProjectInventoryDetailsWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.InventoryGridItem> _projectionGateway;

    public ProjectInventoryDetailsWhenChangedInteractor(IProjectionGateway<Projection.InventoryGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.InventoryCreated @event, CancellationToken cancellationToken)
    {
        Projection.InventoryGridItem card = new(
            @event.InventoryId,
            @event.OwnerId,
            false);

        await _projectionGateway.UpsertAsync(card, cancellationToken);
    }
}