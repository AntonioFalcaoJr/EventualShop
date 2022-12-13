using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public class CatalogDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted>
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;
    private readonly IProjectionGateway<Projection.CatalogItem> _projectionGatewayCatalogItem;

    public CatalogDeletedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway, IProjectionGateway<Projection.CatalogItem> projectionGatewayCatalogItem)
    {
        _projectionGateway = projectionGateway;
        _projectionGatewayCatalogItem = projectionGatewayCatalogItem;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            _projectionGateway.DeleteAsync(@event.CatalogId, cancellationToken),
            _projectionGatewayCatalogItem.DeleteAsync(item => item.CatalogId == @event.CatalogId, cancellationToken)
        );
    }
}