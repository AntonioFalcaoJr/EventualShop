﻿using Application.Abstractions;
using Contracts.Services.Catalog;

namespace Application.UseCases.Events;

public interface IProjectCatalogItemCardWhenCatalogChangedInteractor : IInteractor<DomainEvent.CatalogItemAdded> { }

public class ProjectCatalogItemCardWhenCatalogChangedInteractor : IProjectCatalogItemCardWhenCatalogChangedInteractor
{
    private readonly IProjectionGateway<Projection.CatalogItemCard> _projectionGateway;

    public ProjectCatalogItemCardWhenCatalogChangedInteractor(IProjectionGateway<Projection.CatalogItemCard> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.CatalogItemAdded @event, CancellationToken cancellationToken)
    {
        Projection.CatalogItemCard card = new(
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