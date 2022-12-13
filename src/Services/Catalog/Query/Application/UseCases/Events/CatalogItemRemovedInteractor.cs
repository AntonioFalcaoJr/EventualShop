using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public class CatalogItemRemovedInteractor : IInteractor<DomainEvent.CatalogItemRemoved>
{
    private readonly IProjectionGateway<Projection.CatalogItem> _projectionGateway;

    public CatalogItemRemovedInteractor(IProjectionGateway<Projection.CatalogItem> projectionGateway)
        => _projectionGateway = projectionGateway;

    public async Task InteractAsync(DomainEvent.CatalogItemRemoved @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.ItemId, cancellationToken);

}