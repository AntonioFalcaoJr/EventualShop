using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IDeleteCatalogWhenDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted> { }

public class DeleteCatalogWhenDeletedInteractor : IDeleteCatalogWhenDeletedInteractor
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public DeleteCatalogWhenDeletedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
    {
        await _projectionGateway.DeleteAsync(@event.CatalogId, cancellationToken);
    }
}