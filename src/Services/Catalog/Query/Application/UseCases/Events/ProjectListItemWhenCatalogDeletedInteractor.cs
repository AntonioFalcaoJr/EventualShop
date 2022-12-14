using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectListItemWhenCatalogDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted> { }

public class ProjectListItemWhenCatalogDeletedInteractor : IProjectListItemWhenCatalogDeletedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ProjectListItemWhenCatalogDeletedInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway )
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(item => item.CatalogId == @event.CatalogId, cancellationToken);
}