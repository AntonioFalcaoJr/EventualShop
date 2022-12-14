using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectDetailsWhenCatalogCreatedInteractor : IInteractor<DomainEvent.CatalogCreated> { }

public class ProjectDetailsWhenCatalogCreatedInteractor : IProjectDetailsWhenCatalogCreatedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogDetails> _projectionGateway;

    public ProjectDetailsWhenCatalogCreatedInteractor(IProjectionGateway<Projection.CatalogDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogCreated @event, CancellationToken cancellationToken)
    {
        Projection.CatalogDetails details = new(
            @event.CatalogId,
            @event.Title,
            @event.Description,
            default,
            default);

        await _projectionGateway.InsertAsync(details, cancellationToken);
    }
}