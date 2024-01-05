using Fluxor;

namespace WebAPP.Store.Cataloging.Events;

public record CatalogCreationAborted;

public class CatalogCreationAbortedReducer : Reducer<CatalogingState, CatalogCreationAborted>
{
    public override CatalogingState Reduce(CatalogingState state, CatalogCreationAborted action)
        => state with { IsCreating = false, NewCatalog = new(), Error = string.Empty };
}