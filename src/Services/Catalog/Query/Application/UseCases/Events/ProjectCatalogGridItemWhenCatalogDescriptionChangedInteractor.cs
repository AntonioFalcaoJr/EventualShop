using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogGridItemWhenCatalogDescriptionChangedInteractor : IInteractor<DomainEvent.CatalogDescriptionChanged> { }

public class ProjectCatalogGridItemWhenCatalogDescriptionChangedInteractor : IProjectCatalogGridItemWhenCatalogDescriptionChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogGridItemWhenCatalogDescriptionChangedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDescriptionChanged @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.Description,
            value: @event.Description,
            cancellationToken);
}