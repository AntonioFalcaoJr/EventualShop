using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogItemDetailsWhenCatalogChangedInteractor : IInteractor<DomainEvent.CatalogItemAdded> { }

public class ProjectCatalogItemDetailsWhenCatalogChangedInteractor : IProjectCatalogItemDetailsWhenCatalogChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemDetails> _projectionGateway;

    public ProjectCatalogItemDetailsWhenCatalogChangedInteractor(IProjectionGateway<Projection.CatalogItemDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogItemAdded @event, CancellationToken cancellationToken)
    {
        Projection.CatalogItemDetails card = new(
            @event.ItemId,
            @event.CatalogId,
            @event.Product,
            @event.UnitPrice,
            "image url",
            false,
            @event.Version);

        await _projectionGateway.ReplaceInsertAsync(card, cancellationToken);
    }
}