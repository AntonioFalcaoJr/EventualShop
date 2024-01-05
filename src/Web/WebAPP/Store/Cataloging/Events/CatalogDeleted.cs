using Fluxor;
using WebAPP.Abstractions;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogDeleted(string CatalogId);

public class CatalogDeletedReducer : Reducer<CatalogingState, CatalogDeleted>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogDeleted @event)
    {
        var catalogs = state.Catalogs.RemoveAll(c => c.CatalogId == @event.CatalogId);

        Page page = new() { HasNext = catalogs.Count > state.Page.Size };
        // if (page.HasNext) catalogs = catalogs.RemoveAt(catalogs.Count - 1);

        return state with { IsDeleting = false, Catalogs = catalogs, Page = page };
    }
}