using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogDetailsWhenCatalogDeactivatedInteractor : IInteractor<DomainEvent.CatalogDeactivated> { }

public class ProjectCatalogDetailsWhenCatalogDeactivatedInteractor : IProjectCatalogDetailsWhenCatalogDeactivatedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogDetailsWhenCatalogDeactivatedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
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