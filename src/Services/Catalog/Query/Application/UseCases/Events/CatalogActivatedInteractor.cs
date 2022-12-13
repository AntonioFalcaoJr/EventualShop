using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public class CatalogActivatedInteractor : IInteractor<DomainEvent.CatalogActivated>
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public CatalogActivatedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
        => _projectionGateway = projectionGateway;

    public async Task InteractAsync(DomainEvent.CatalogActivated @event, CancellationToken cancellationToken)
      => await _projectionGateway.UpdateFieldAsync(
          id: @event.CatalogId,
          field: catalog => catalog.IsActive,
          value: true,
          cancellationToken);
}