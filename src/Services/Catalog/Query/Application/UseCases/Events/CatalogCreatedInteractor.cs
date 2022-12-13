using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public class CatalogCreatedInteractor : IInteractor<DomainEvent.CatalogCreated>
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public CatalogCreatedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
        => _projectionGateway = projectionGateway;

    public async Task InteractAsync(DomainEvent.CatalogCreated @event, CancellationToken cancellationToken)
    {
        Projection.Catalog catalog = new(
            @event.CatalogId,
            @event.Title,
            @event.Description,
            default,
            default);

        await _projectionGateway.InsertAsync(catalog, cancellationToken);
    }
}