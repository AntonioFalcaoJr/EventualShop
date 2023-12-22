using System.Collections.Immutable;
using Fluxor;
using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging.Events;

public record ProductsSearchHit
{
    public required IPagedResult<Product> Products;
    
    [ReducerMethod]
    public static CatalogingState ProductsSearchHitReducer(CatalogingState state, ProductsSearchHit @event)
        => state with { Products = @event.Products.Items.ToImmutableList(), IsSearching = false, Error = string.Empty };
}