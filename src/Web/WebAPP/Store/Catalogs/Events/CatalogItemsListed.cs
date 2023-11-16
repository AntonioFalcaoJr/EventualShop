using Fluxor;
using WebAPP.Abstractions;
using System.Collections.Immutable;

namespace WebAPP.Store.Catalogs.Events;

public record CatalogItemsListed
{
    public required IPagedResult<CatalogItem> PagedResult;
    
    [ReducerMethod]
    public static CatalogState Reduce(CatalogState state, CatalogItemsListed @event)
        => state with
        {
            IsFetching = false,
            HasError = false,
            Page = @event.PagedResult.Page,
            // TODO, improve this to avoid this .ToImmutableList()
            Items = @event.PagedResult.Items.ToImmutableList()
        };
}