using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public class CatalogDeactivatedInteractor : IInteractor<DomainEvent.CatalogDeactivated>
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public CatalogDeactivatedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
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