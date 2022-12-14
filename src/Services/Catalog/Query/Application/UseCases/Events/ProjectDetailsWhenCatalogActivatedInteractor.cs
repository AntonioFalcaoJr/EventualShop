using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogDetailsWhenCatalogActivatedInteractor : IInteractor<DomainEvent.CatalogActivated> { }

public class ProjectCatalogDetailsWhenCatalogActivatedInteractor : IProjectCatalogDetailsWhenCatalogActivatedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogDetailsWhenCatalogActivatedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogActivated @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken);
}