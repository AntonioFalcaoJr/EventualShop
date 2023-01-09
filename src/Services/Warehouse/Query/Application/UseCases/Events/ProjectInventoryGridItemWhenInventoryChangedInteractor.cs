using Application.Abstractions;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Events;

public interface IProjectInventoryGridItemWhenInventoryChangedInteractor : IInteractor<DomainEvent.InventoryCreated> { }

public class ProjectInventoryGridItemWhenInventoryChangedInteractor : IProjectInventoryGridItemWhenInventoryChangedInteractor
{
    private readonly IProjectionGateway<Projection.InventoryGridItem> _projectionGateway;

    public ProjectInventoryGridItemWhenInventoryChangedInteractor(IProjectionGateway<Projection.InventoryGridItem> projectionGateway)
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