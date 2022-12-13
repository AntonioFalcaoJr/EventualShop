using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public class CatalogItemAddedInteractor : IInteractor<DomainEvent.CatalogItemAdded>
{
    private readonly IProjectionGateway<Projection.CatalogItem> _projectionGateway;

    public CatalogItemAddedInteractor(IProjectionGateway<Projection.CatalogItem> projectionGateway)
        => _projectionGateway = projectionGateway;

    public async Task InteractAsync(DomainEvent.CatalogItemAdded @event, CancellationToken cancellationToken)
    {
        Projection.CatalogItem catalogItem = new(
            @event.CatalogId,
            @event.ItemId,
            @event.CatalogId,
            @event.Product,
            default);

        await _projectionGateway.InsertAsync(catalogItem, cancellationToken);
    }
      
}