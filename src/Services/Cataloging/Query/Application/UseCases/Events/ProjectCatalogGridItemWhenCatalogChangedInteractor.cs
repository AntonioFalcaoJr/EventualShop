using Application.Abstractions;
using Contracts.Boundaries.Cataloging.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogGridItemWhenCatalogChangedInteractor :
    IInteractor<DomainEvent.CatalogActivated>,
    IInteractor<DomainEvent.CatalogCreated>,
    IInteractor<DomainEvent.CatalogInactivated>,
    IInteractor<DomainEvent.CatalogDescriptionChanged>,
    IInteractor<DomainEvent.CatalogTitleChanged>,
    IInteractor<DomainEvent.CatalogDeleted>;

public class ProjectCatalogGridItemWhenCatalogChangedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    : IProjectCatalogGridItemWhenCatalogChangedInteractor
{
    public async Task InteractAsync(DomainEvent.CatalogActivated @event, CancellationToken cancellationToken)
        => await projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            version: @event.Version,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogCreated @event, CancellationToken cancellationToken)
    {
        Projection.CatalogGridItem gridItem = new(
            @event.CatalogId,
            @event.Title,
            @event.Description,
            "image url", // TODO: get image url from event
            default,
            default,
            @event.Version);

        await projectionGateway.ReplaceInsertAsync(gridItem, cancellationToken);
    }

    public async Task InteractAsync(DomainEvent.CatalogInactivated @event, CancellationToken cancellationToken)
        => await projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            version: @event.Version,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogDescriptionChanged @event, CancellationToken cancellationToken)
        => await projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            version: @event.Version,
            field: catalog => catalog.Description,
            value: @event.Description,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogTitleChanged @event, CancellationToken cancellationToken)
        => await projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            version: @event.Version,
            field: catalog => catalog.Title,
            value: @event.Title,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await projectionGateway.DeleteAsync(@event.CatalogId, cancellationToken);
}