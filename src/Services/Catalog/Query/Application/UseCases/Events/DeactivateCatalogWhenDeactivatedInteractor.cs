using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IDeactivateCatalogWhenDeactivatedInteractor : IInteractor<DomainEvent.CatalogDeactivated> { }

public class DeactivateCatalogWhenDeactivatedInteractor : IDeactivateCatalogWhenDeactivatedInteractor
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public DeactivateCatalogWhenDeactivatedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeactivated @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken);
}