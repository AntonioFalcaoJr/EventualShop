using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IDescriptionChangeCatalogWhenChangedInteractor : IInteractor<DomainEvent.CatalogDescriptionChanged> { }

public class DescriptionChangeCatalogWhenChangedInteractor : IDescriptionChangeCatalogWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.Catalog> _projectionGateway;

    public DescriptionChangeCatalogWhenChangedInteractor(IProjectionGateway<Projection.Catalog> projectionGateway)
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