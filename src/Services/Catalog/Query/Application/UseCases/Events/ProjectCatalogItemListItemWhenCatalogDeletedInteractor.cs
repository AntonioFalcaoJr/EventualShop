using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogItemListItemWhenCatalogDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted> { }

public class ProjectCatalogItemListItemWhenCatalogDeletedInteractor : IProjectCatalogItemListItemWhenCatalogDeletedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ProjectCatalogItemListItemWhenCatalogDeletedInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway )
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(item => item.CatalogId == @event.CatalogId, cancellationToken);
}