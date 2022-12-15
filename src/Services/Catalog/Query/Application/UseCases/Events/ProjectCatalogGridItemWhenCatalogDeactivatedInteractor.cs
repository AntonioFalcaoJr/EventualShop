using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogGridItemWhenCatalogDeactivatedInteractor : IInteractor<DomainEvent.CatalogDeactivated> { }

public class ProjectCatalogGridItemWhenCatalogDeactivatedInteractor : IProjectCatalogGridItemWhenCatalogDeactivatedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogGridItemWhenCatalogDeactivatedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeactivated @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken);
}