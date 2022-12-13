using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public class CatalogTitleChangedInteractor : IInteractor<DomainEvent.CatalogTitleChanged>
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public CatalogTitleChangedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogTitleChanged @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.Title,
            value: @event.Title,
            cancellationToken);
}