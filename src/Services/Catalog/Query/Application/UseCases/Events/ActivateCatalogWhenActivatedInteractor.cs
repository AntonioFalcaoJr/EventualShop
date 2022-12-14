using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IActivateCatalogWhenActivatedInteractor : IInteractor<DomainEvent.CatalogActivated> { }

public class ActivateCatalogWhenActivatedInteractor : IActivateCatalogWhenActivatedInteractor
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public ActivateCatalogWhenActivatedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
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