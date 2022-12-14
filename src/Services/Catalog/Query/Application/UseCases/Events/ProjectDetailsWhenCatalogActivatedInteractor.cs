using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectDetailsWhenCatalogActivatedInteractor : IInteractor<DomainEvent.CatalogActivated> { }

public class ProjectDetailsWhenCatalogActivatedInteractor : IProjectDetailsWhenCatalogActivatedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogDetails> _projectionGateway;

    public ProjectDetailsWhenCatalogActivatedInteractor(IProjectionGateway<Projection.CatalogDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogActivated @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.IsActive,
            value: true,
            cancellationToken);
}