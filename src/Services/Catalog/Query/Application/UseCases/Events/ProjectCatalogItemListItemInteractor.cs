using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogItemListItemInteractor :
    IInteractor<DomainEvent.CatalogDeleted>,
    IInteractor<DomainEvent.CatalogItemAdded>,
    IInteractor<DomainEvent.CatalogItemRemoved> { }

public class ProjectCatalogItemListItemInteractor : IProjectCatalogItemListItemInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ProjectCatalogItemListItemInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
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

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(item => item.CatalogId == @event.CatalogId, cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogItemRemoved @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.ItemId, cancellationToken);
}