using Application.Abstractions;
using Contracts.Boundaries.Warehouse;

namespace Application.UseCases.Events;

public interface IProjectInventoryGridItemWhenInventoryChangedInteractor : IInteractor<DomainEvent.InventoryCreated> { }

public class ProjectInventoryGridItemWhenInventoryChangedInteractor(IProjectionGateway<Projection.InventoryGridItem> projectionGateway)
    : IProjectInventoryGridItemWhenInventoryChangedInteractor
{
    public async Task InteractAsync(DomainEvent.InventoryCreated @event, CancellationToken cancellationToken)
    {
        Projection.InventoryGridItem card = new(
            @event.InventoryId,
            @event.OwnerId,
            false,
            @event.Version);

        await projectionGateway.ReplaceInsertAsync(card, cancellationToken);
    }
}