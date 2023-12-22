using System.Collections.Immutable;
using Fluxor;
using WebAPP.Abstractions;
using WebAPP.Store.Catalogs;

namespace WebAPP.Store.Cataloging;

[FeatureState]
public record CatalogingState
{
    public IImmutableList<Catalog> Catalogs { get; init; } = ImmutableList<Catalog>.Empty;
    public Catalog NewCatalog { get; init; } = new();
    public CatalogItem NewItem { get; init; } = new();
    public bool IsCreating { get; init; }
    public bool IsFetching { get; init; }
    public bool IsDeleting { get; init; }
    public bool IsEditingTitle { get; init; }
    public bool IsAddingItem { get; init; }
    public bool IsEditingDescription { get; init; }
    public string Error { get; init; } = string.Empty;
    public Page Page { get; init; } = new();
    public bool IsCreatingItem { get; set; }
    public string CatalogId { get; set; } = "Undefined";
    public bool HasError => Error != string.Empty;

    // It is necessary to have a separate property for the searching state 
    public bool IsSearching { get; set; }
    public string Fragment { get; set; } = string.Empty;
    public IImmutableList<Product> Products { get; set; } = ImmutableList<Product>.Empty;
    ////
}

public record Catalog
{
    public bool IsActive { get; set; }
    public string CatalogId { get; set; } = "Undefined";
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}