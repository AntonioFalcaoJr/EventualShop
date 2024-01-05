using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogCreationFailed(string Error);

public class CatalogCreationFailedReducer : Reducer<CatalogingState, CatalogCreationFailed>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogCreationFailed action)
        => state with { IsCreating = false, Error = action.Error, NewCatalog = new() };
}