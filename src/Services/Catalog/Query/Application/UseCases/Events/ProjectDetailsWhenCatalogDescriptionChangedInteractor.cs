using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectDetailsWhenCatalogDescriptionChangedInteractor : IInteractor<DomainEvent.CatalogDescriptionChanged> { }

public class ProjectDetailsWhenCatalogDescriptionChangedInteractor : IProjectDetailsWhenCatalogDescriptionChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogDetails> _projectionGateway;

    public ProjectDetailsWhenCatalogDescriptionChangedInteractor(IProjectionGateway<Projection.CatalogDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogDescriptionChanged @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.Description,
            value: @event.Description,
            cancellationToken);
}