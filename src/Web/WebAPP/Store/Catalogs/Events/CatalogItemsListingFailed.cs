using Fluxor;

namespace WebAPP.Store.Catalogs.Events;

public record CatalogItemsListingFailed(string Error);

public class CatalogItemsListingFailedReducer : Reducer<CatalogState, CatalogItemsListingFailed>
{
    public override CatalogState Reduce(CatalogState state, CatalogItemsListingFailed @event)
        => state with
        {
            IsFetching = false, 
            HasError = true, 
            Error = @event.Error
        };
}