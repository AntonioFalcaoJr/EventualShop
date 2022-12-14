using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectDetailsWhenCatalogDeletedInteractor : IInteractor<DomainEvent.CatalogDeleted> { }

public class ProjectDetailsWhenCatalogDeletedInteractor : IProjectDetailsWhenCatalogDeletedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogDetails> _projectionGateway;

    public ProjectDetailsWhenCatalogDeletedInteractor(IProjectionGateway<Projection.CatalogDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDeleted @event, CancellationToken cancellationToken)
        => await _projectionGateway.DeleteAsync(@event.CatalogId, cancellationToken);
}