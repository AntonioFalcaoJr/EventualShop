using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogCreationAborted
{
    [ReducerMethod]
    public static CatalogingState CatalogCreationAbortedReducer(CatalogingState state, CatalogCreationAborted _)
        => state with { IsCreating = false, NewCatalog = new(), Error = string.Empty };
}