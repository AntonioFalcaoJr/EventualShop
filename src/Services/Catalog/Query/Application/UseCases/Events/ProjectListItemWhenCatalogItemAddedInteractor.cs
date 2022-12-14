using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogItemListItemWhenCatalogItemAddedInteractor : IInteractor<DomainEvent.CatalogItemAdded> { }

public class ProjectCatalogItemListItemWhenCatalogItemAddedInteractor : IProjectCatalogItemListItemWhenCatalogItemAddedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ProjectCatalogItemListItemWhenCatalogItemAddedInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogItemAdded @event, CancellationToken cancellationToken)
    {
        Projection.CatalogItemListItem listItem = new(
            @event.ItemId,
            @event.CatalogId,
            @event.Product,
            false);

        await _projectionGateway.InsertAsync(listItem, cancellationToken);
    }
}