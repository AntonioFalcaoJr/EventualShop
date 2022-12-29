using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogGridItemWhenCatalogCreatedInteractor : IInteractor<DomainEvent.CatalogCreated> { }

public class ProjectCatalogGridItemWhenCatalogCreatedInteractor : IProjectCatalogGridItemWhenCatalogCreatedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogGridItemWhenCatalogCreatedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogCreated @event, CancellationToken cancellationToken)
    {
        Projection.CatalogGridItem gridItem = new(
            @event.CatalogId,
            @event.Title,
            @event.Description,
            "image url", // TODO: get image url from event
            default,
            default);

        await _projectionGateway.InsertAsync(gridItem, cancellationToken);
    }
}