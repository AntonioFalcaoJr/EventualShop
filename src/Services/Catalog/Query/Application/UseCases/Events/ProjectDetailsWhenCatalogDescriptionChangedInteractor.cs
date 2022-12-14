using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogDetailsWhenCatalogDescriptionChangedInteractor : IInteractor<DomainEvent.CatalogDescriptionChanged> { }

public class ProjectCatalogDetailsWhenCatalogDescriptionChangedInteractor : IProjectCatalogDetailsWhenCatalogDescriptionChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogDetailsWhenCatalogDescriptionChangedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
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