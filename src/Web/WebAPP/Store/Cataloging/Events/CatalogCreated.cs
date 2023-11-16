using Fluxor;
using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogCreated
{
    public required Catalog NewCatalog;

    [ReducerMethod]
    public static CatalogingState Reduce(CatalogingState state, CatalogCreated @event)
    {
        var catalogs = state.Catalogs.Insert(0, @event.NewCatalog);

        Page page = new() { HasNext = catalogs.Count > state.Page.Size };
        if (page.HasNext) catalogs = catalogs.RemoveAt(catalogs.Count - 1);

        return state with { IsCreating = false, NewCatalog = new(), Catalogs = catalogs, Page = page };
    }
}