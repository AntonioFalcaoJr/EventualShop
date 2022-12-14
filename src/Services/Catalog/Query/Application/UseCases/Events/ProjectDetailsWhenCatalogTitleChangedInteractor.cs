using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogDetailsWhenCatalogTitleChangedInteractor : IInteractor<DomainEvent.CatalogTitleChanged> { }

public class ProjectCatalogDetailsWhenCatalogTitleChangedInteractor : IProjectCatalogDetailsWhenCatalogTitleChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogDetailsWhenCatalogTitleChangedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogTitleChanged @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.Title,
            value: @event.Title,
            cancellationToken);
}