using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IRemoveCatalogItemWhenRemovedInteractor : IInteractor<DomainEvent.CatalogItemRemoved> { }

public class RemoveCatalogItemWhenRemovedInteractor : IRemoveCatalogItemWhenRemovedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItem> _projectionGateway;

    public RemoveCatalogItemWhenRemovedInteractor(IProjectionGateway<Projection.CatalogItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogItemRemoved @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.ItemId, cancellationToken);
}