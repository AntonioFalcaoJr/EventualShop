using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectListItemWhenCatalogItemAddedInteractor : IInteractor<DomainEvent.CatalogItemAdded> { }

public class ProjectListItemWhenCatalogItemAddedInteractor : IProjectListItemWhenCatalogItemAddedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ProjectListItemWhenCatalogItemAddedInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogItemAdded @event, CancellationToken cancellationToken)
    {
        Projection.CatalogItemListItem listItem = new(
            @event.CatalogId,
            @event.ItemId,
            @event.CatalogId,
            @event.Product,
            default);

        await _projectionGateway.InsertAsync(listItem, cancellationToken);
    }
}