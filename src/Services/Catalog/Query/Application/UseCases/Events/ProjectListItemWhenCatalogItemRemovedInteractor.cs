using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogItemListItemWhenCatalogItemRemovedInteractor : IInteractor<DomainEvent.CatalogItemRemoved> { }

public class ProjectCatalogItemListItemWhenCatalogItemRemovedInteractor : IProjectCatalogItemListItemWhenCatalogItemRemovedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ProjectCatalogItemListItemWhenCatalogItemRemovedInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogItemRemoved @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.ItemId, cancellationToken);
}