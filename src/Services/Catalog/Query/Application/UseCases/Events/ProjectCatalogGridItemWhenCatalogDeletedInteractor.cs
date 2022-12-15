using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogGridItemWhenCatalogDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted> { }

public class ProjectCatalogGridItemWhenCatalogDeletedInteractor : IProjectCatalogGridItemWhenCatalogDeletedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogGridItemWhenCatalogDeletedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.CatalogId, cancellationToken);
}