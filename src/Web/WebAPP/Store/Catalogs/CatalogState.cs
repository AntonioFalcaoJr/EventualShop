using System.Collections.Immutable;
using Contracts.Boundaries.Cataloging.Catalog;
using Fluxor;
using WebAPP.Abstractions;

namespace WebAPP.Store.Catalogs;

[FeatureState]
public record CatalogState
{
    public string Id { get; init; } = string.Empty;
    public IImmutableList<CatalogItem> Items { get; init; } = ImmutableList<CatalogItem>.Empty;
    public bool IsFetching { get; init; }
    public bool HasError { get; init; }
    public string Error { get; init; } = string.Empty;
    public Page Page { get; init; } = new();
}

public record CatalogItem
{
    public string Id { get; init; } = string.Empty;
    public string ProductId { get; private init; } = string.Empty;
    public string Name { get; private init; } = string.Empty;
    public string Description { get; private init; } = string.Empty;

    public static implicit operator CatalogItem(Projection.CatalogItemListItem item)
        => new()
        {
            Id = item.Id.ToString(),
            ProductId = item.ProductId.ToString(),
            Name = item.Product.Name,
            Description = "Description static on client side" // TODO: solve this
        };
}