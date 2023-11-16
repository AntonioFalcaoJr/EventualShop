using System.Collections.Immutable;
using Fluxor;
using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging;

[FeatureState]
public record CatalogingState
{
    public IImmutableList<Catalog> Catalogs { get; init; } = ImmutableList<Catalog>.Empty;
    public Catalog NewCatalog { get; init; } = new();
    public bool IsCreating { get; init; }
    public bool IsFetching { get; init; }
    public bool IsDeleting { get; init; }
    public bool IsEditingTitle { get; init; }
    public bool IsEditingDescription { get; init; }
    public bool HasError => Error != string.Empty;
    public string Error { get; init; } = string.Empty;
    public Page Page { get; init; } = new();
}

public record Catalog
{
    public bool IsActive { get; set; }
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}