using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogGridItemWhenCatalogChangedInteractor :
    IInteractor<DomainEvent.CatalogActivated>,
    IInteractor<DomainEvent.CatalogCreated>,
    IInteractor<DomainEvent.CatalogDeactivated>,
    IInteractor<DomainEvent.CatalogDescriptionChanged>,
    IInteractor<DomainEvent.CatalogTitleChanged>,
    IInteractor<DomainEvent.CatalogDeleted> { }

public class ProjectCatalogGridItemWhenCatalogChangedInteractor : IProjectCatalogGridItemWhenCatalogChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogGridItem> _projectionGateway;

    public ProjectCatalogGridItemWhenCatalogChangedInteractor(IProjectionGateway<Projection.CatalogGridItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogActivated @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
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
            default);

        await _projectionGateway.UpsertAsync(gridItem, cancellationToken);
    }

    public async Task InteractAsync(DomainEvent.CatalogDeactivated @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.IsActive,
            value: false,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogDescriptionChanged @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.Description,
            value: @event.Description,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogTitleChanged @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.Title,
            value: @event.Title,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.CatalogId, cancellationToken);
}