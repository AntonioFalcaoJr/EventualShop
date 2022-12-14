using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectDetailsWhenCatalogTitleChangedInteractor : IInteractor<DomainEvent.CatalogTitleChanged> { }

public class ProjectDetailsWhenCatalogTitleChangedInteractor : IProjectDetailsWhenCatalogTitleChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogDetails> _projectionGateway;

    public ProjectDetailsWhenCatalogTitleChangedInteractor(IProjectionGateway<Projection.CatalogDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogTitleChanged @event, CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.CatalogId,
            field: catalog => catalog.Title,
            value: @event.Title,
            cancellationToken);
}