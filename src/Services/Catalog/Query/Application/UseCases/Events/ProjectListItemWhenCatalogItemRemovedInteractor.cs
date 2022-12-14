using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectListItemWhenCatalogItemRemovedInteractor : IInteractor<DomainEvent.CatalogItemRemoved> { }

public class ProjectListItemWhenCatalogItemRemovedInteractor : IProjectListItemWhenCatalogItemRemovedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemListItem> _projectionGateway;

    public ProjectListItemWhenCatalogItemRemovedInteractor(IProjectionGateway<Projection.CatalogItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogItemRemoved @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.ItemId, cancellationToken);
}