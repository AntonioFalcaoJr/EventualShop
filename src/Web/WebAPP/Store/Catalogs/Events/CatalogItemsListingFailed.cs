using Fluxor;

namespace WebAPP.Store.Catalogs.Events;

public record CatalogItemsListingFailed
{
    public required string Error;

    [ReducerMethod]
    public static CatalogState Reduce(CatalogState state, CatalogItemsListingFailed @event)
        => state with { IsFetching = false, HasError = true, Error = @event.Error };
}