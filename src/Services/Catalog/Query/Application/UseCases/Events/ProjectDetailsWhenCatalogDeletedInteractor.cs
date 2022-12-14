using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogDetailsWhenCatalogDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted> { }

public class ProjectCatalogDetailsWhenCatalogDeletedInteractor : IProjectCatalogDetailsWhenCatalogDeletedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogDetailsWhenCatalogDeletedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.CatalogId, cancellationToken);
}