using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IDeleteCatalogItemWhenCatalogDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted> { }

public class DeleteCatalogItemWhenCatalogDeletedInteractor : IDeleteCatalogItemWhenCatalogDeletedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItem> _projectionGateway;

    public DeleteCatalogItemWhenCatalogDeletedInteractor(IProjectionGateway<Projection.CatalogItem> projectionGateway )
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
    {
        await _projectionGateway.DeleteAsync(item => item.CatalogId == @event.CatalogId, cancellationToken);
    }
}